﻿// ------------------------------------------------------------------------------
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

using Humanizer;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
using War3Api.Generator.Object.Models;

using War3Net.Common.Extensions;
using War3Net.IO.Slk;

namespace War3Api.Generator.Object
{
    internal static class AbilityApiGenerator
    {
        private const bool IsAbilityClassAbstract = true;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            var abilityData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.AbilityDataPath))).Shrink();
            var abilityMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.AbilityMetaDataPath))));

            Generate(abilityData, abilityMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            // Data columns
            var abilityIdColumn = data[DataConstants.AbilityDataKeyColumn].Single();
            var commentColumn = data[DataConstants.CommentsColumn].Single();

            // MetaData columns
            var idColumn = metaData[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = metaData[DataConstants.MetaDataFieldColumn].Single();
            var repeatColumn = metaData[DataConstants.MetaDataRepeatColumn].Single();
            var dataColumn = metaData[DataConstants.MetaDataDataColumn].Single();
            var categoryColumn = metaData[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = metaData[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = metaData[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = metaData[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = metaData[DataConstants.MetaDataMaxValColumn].Single();
            var useSpecificColumn = metaData[DataConstants.MetaDataUseSpecificColumn].Single();

            // Properties
            var properties = metaData
                .Skip(1)
                .Select(property => new PropertyModel
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
                        (string)property[fieldColumn],
                        (string)property[categoryColumn],
                        (string)property[displayNameColumn]),
                    Repeat = (int)property[repeatColumn],
                    Data = (int)property[dataColumn],
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    UseSpecific = (string)property[useSpecificColumn],
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);

            // Ability types (enum)
            var abilityTypeEnumModel = new EnumModel(DataConstants.AbilityTypeEnumName);
            foreach (var abilityType in data.Skip(1))
            {
                if (string.IsNullOrEmpty((string)abilityType[abilityIdColumn]))
                {
                    continue;
                }

                abilityTypeEnumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel((string)abilityType[commentColumn], (string)abilityType[abilityIdColumn]));
            }

            abilityTypeEnumModel.EnsureMemberNamesUnique();

            ObjectApiGenerator.GenerateEnumFile(abilityTypeEnumModel);

            // Ability (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.AbilityClassName, DataConstants.AbilityTypeEnumName, properties.Values.Where(property => string.IsNullOrEmpty(property.UseSpecific)), IsAbilityClassAbstract));

            if (!IsAbilityClassAbstract)
            {
                classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.AbilityClassName, DataConstants.AbilityTypeEnumName));
            }

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.AbilityClassName, IsAbilityClassAbstract, DataConstants.BaseClassName, classMembers));

            // Abilities (subclasses)
            if (IsAbilityClassAbstract)
            {
                foreach (var abilityType in abilityTypeEnumModel.Members)
                {
                    ObjectApiGenerator.GenerateMember(
                        SyntaxFactoryService.Class(
                            abilityType.UniqueName,
                            false,
                            DataConstants.AbilityClassName,
                            ObjectApiGenerator.GetProperties(
                                abilityType.UniqueName,
                                DataConstants.AbilityTypeEnumName,
                                properties.Values.Where(property => property.UseSpecific?.Contains(abilityType.Value.ToRawcode(), StringComparison.Ordinal) ?? false),
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
                            FactoryCreateMethod(abilityTypeEnumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                }),

                            FactoryCreateMethod(abilityTypeEnumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("int", "newId"),
                                }),

                            FactoryCreateMethod(abilityTypeEnumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("string", "newRawcode"),
                                }),

                            FactoryCreateMethod(abilityTypeEnumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                                }),

                            FactoryCreateMethod(abilityTypeEnumModel.Members,
                                new[]
                                {
                                    (DataConstants.AbilityTypeEnumName, abilityTypeVariableName),
                                    ("int", "newId"),
                                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                                }),

                            FactoryCreateMethod(abilityTypeEnumModel.Members,
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
                abilityTypeEnumModel.Members,
                properties.Values,
                data,
                DataConstants.AbilityClassName,
                DataConstants.AbilityTypeEnumName,
                DataConstants.AbilityDataKeyColumn,
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