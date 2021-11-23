// ------------------------------------------------------------------------------
// <copyright file="AbilityApiGenerator.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable CA1303 // Do not pass literals as localized parameters

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
using War3Api.Generator.Object.Models;

namespace War3Api.Generator.Object
{
    internal static class AbilityApiGenerator
    {
        private const bool IsAbilityClassAbstract = true;

        private static Dictionary<string, TableModel> _dataTables;
        private static TableModel _metadataTable;

        private static EnumModel _enumModel;

        private static bool _initialized = false;

        internal static void InitializeGenerator(string inputFolder)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Already initialized.");
            }

            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            _dataTables = new[]
            {
                new TableModel(Path.Combine(inputFolder, PathConstants.AbilityDataPath), DataConstants.AbilityDataKeyColumn, DataConstants.CommentsColumn),
            }
            .ToDictionary(table => table.TableName, StringComparer.OrdinalIgnoreCase);

            _metadataTable = new TableModel(Path.Combine(inputFolder, PathConstants.AbilityMetaDataPath));
            ObjectApiGenerator.Localize(_metadataTable.Table);

            _enumModel = new EnumModel(DataConstants.AbilityTypeEnumName);

            var members = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var dataTable in _dataTables.Values)
            {
                dataTable.AddValues(members);
            }

            foreach (var member in members)
            {
                _enumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel(member.Value, member.Key));
            }

            _enumModel.EnsureMemberNamesUnique();

            _initialized = true;
        }

        internal static void Generate()
        {
            // MetaData columns
            var idColumn = _metadataTable.Table[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = _metadataTable.Table[DataConstants.MetaDataFieldColumn].Single();
            var dataSourceColumn = _metadataTable.Table[DataConstants.MetaDataSlkColumn].Single();
            var repeatColumn = _metadataTable.Table[DataConstants.MetaDataRepeatColumn].Single();
            var dataColumn = _metadataTable.Table[DataConstants.MetaDataDataColumn].Single();
            var categoryColumn = _metadataTable.Table[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = _metadataTable.Table[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = _metadataTable.Table[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = _metadataTable.Table[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = _metadataTable.Table[DataConstants.MetaDataMaxValColumn].Single();
            var useSpecificColumn = _metadataTable.Table[DataConstants.MetaDataUseSpecificColumn].Single();

            // Properties
            var properties = _metadataTable.Table
                .Skip(1)
                .Select(property => new PropertyModel
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    DataSource = (string)property[dataSourceColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
                        (string)property[fieldColumn],
                        (string)property[categoryColumn],
                        (string)property[displayNameColumn]),
                    Repeat = (int)property[repeatColumn],
                    Data = (int)property[dataColumn],
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Specifics = ((string)property[useSpecificColumn]).GetSpecifics(),
                    SpecificUniqueNames = new(),
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values, useSpecific: true);

            // Ability types (enum)
            ObjectApiGenerator.GenerateEnumFile(_enumModel);

            // Ability (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.AbilityClassName, DataConstants.AbilityTypeEnumName, properties.Values.Where(propertyModel => propertyModel.Specifics.IsEmpty), IsAbilityClassAbstract));

            if (!IsAbilityClassAbstract)
            {
                classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.AbilityClassName, DataConstants.AbilityTypeEnumName));
            }

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.AbilityClassName, IsAbilityClassAbstract, DataConstants.BaseClassName, classMembers));

            // Abilities (subclasses)
            if (IsAbilityClassAbstract)
            {
                foreach (var abilityType in _enumModel.Members)
                {
                    ObjectApiGenerator.GenerateMember(
                        SyntaxFactoryService.Class(
                            abilityType.UniqueName,
                            false,
                            DataConstants.AbilityClassName,
                            ObjectApiGenerator.GetProperties(
                                abilityType.UniqueName,
                                DataConstants.AbilityTypeEnumName,
                                properties.Values.Where(propertyModel => propertyModel.Specifics.Contains(abilityType.Value)),
                                false,
                                false,
                                abilityType.Value)),
                        DataConstants.AbilityNamespace);
                }

                var abilityTypeVariableName = DataConstants.AbilityTypeEnumName.ToCamelCase(true);
                ObjectApiGenerator.GenerateMember(
                    SyntaxFactoryService.Class(
                        $"{DataConstants.AbilityClassName}Factory",
                        new[] { SyntaxKind.PublicKeyword, SyntaxKind.StaticKeyword },
                        null,
                        new[]
                        {
                            FactoryCreateMethod(_enumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                }),

                            FactoryCreateMethod(_enumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("int", "newId"),
                                }),

                            FactoryCreateMethod(_enumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("string", "newRawcode"),
                                }),

                            FactoryCreateMethod(_enumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                                }),

                            FactoryCreateMethod(_enumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("int", "newId"),
                                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                                }),

                            FactoryCreateMethod(_enumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("string", "newRawcode"),
                                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                                }),
                        }));
            }

            // AbilityLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                _enumModel.Members,
                properties.Values,
                _dataTables,
                DataConstants.AbilityClassName,
                DataConstants.AbilityTypeEnumName,
                IsAbilityClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.AbilityClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }

        private static MethodDeclarationSyntax FactoryCreateMethod(IEnumerable<EnumMemberModel> members, IEnumerable<(string type, string identifier)> parameters)
        {
            var switchExpressionArms = members
                .Select(abilityType => SyntaxFactory.SwitchExpressionArm(
                    SyntaxFactory.ConstantPattern(SyntaxFactory.ParseExpression($"{DataConstants.AbilityTypeEnumName}.{abilityType.UniqueName}")),
                    SyntaxFactory.ParseExpression($"new {abilityType.UniqueName}({string.Join(", ", parameters.Skip(1).Select(parameter => parameter.identifier))})")))
                .ToList();

            var abilityTypeIdentifier = parameters.First().identifier;
            switchExpressionArms.Add(SyntaxFactory.SwitchExpressionArm(
                SyntaxFactory.DiscardPattern(),
                SyntaxFactory.ParseExpression($"throw new System.ComponentModel.InvalidEnumArgumentException(nameof({abilityTypeIdentifier}), (int){abilityTypeIdentifier}, typeof({DataConstants.AbilityTypeEnumName}))")));

            return SyntaxFactoryService.Method(
                new[] { SyntaxKind.PublicKeyword, SyntaxKind.StaticKeyword },
                DataConstants.AbilityClassName,
                "Create",
                parameters,
                new[]
                {
                    SyntaxFactory.ReturnStatement(SyntaxFactory.SwitchExpression(
                        SyntaxFactory.ParseExpression(abilityTypeIdentifier),
                        SyntaxFactory.SeparatedList(switchExpressionArms))),
                });
        }
    }
}