// ------------------------------------------------------------------------------
// <copyright file="UnitApiGenerator.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable CA1303 // Do not pass literals as localized parameters

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
using War3Api.Generator.Object.Models;

namespace War3Api.Generator.Object
{
    internal static class UnitApiGenerator
    {
        private const bool IsUnitClassAbstract = false;

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
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitAbilityDataPath), DataConstants.UnitAbilityDataKeyColumn, DataConstants.CommentOrCommentsColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitBalanceDataPath), DataConstants.UnitBalanceDataKeyColumn, DataConstants.CommentOrCommentsColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitDataPath), DataConstants.UnitDataKeyColumn, DataConstants.CommentOrCommentsColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitUiDataPath), DataConstants.UnitUiDataKeyColumn, DataConstants.UnitDataNameColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitWeaponDataPath), DataConstants.UnitWeaponDataKeyColumn, DataConstants.CommentOrCommentsColumn),
            }
            .ToDictionary(table => table.TableName, StringComparer.OrdinalIgnoreCase);

            _metadataTable = new TableModel(Path.Combine(inputFolder, PathConstants.UnitMetaDataPath));
            ObjectApiGenerator.Localize(_metadataTable.Table);

            _enumModel = new EnumModel(DataConstants.UnitTypeEnumName);

            var members = new Dictionary<string, string>(StringComparer.Ordinal);

            _dataTables["unitui"].AddValues(members);
            _dataTables["unitdata"].AddValues(members);

            _dataTables["unitabilities"].AddValues(members);
            _dataTables["unitbalance"].AddValues(members);
            _dataTables["unitweapons"].AddValues(members);

            foreach (var member in members)
            {
                _enumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel(member.Value, member.Key));
            }

            _enumModel.EnsureMemberNamesUnique();

            _initialized = true;
        }

        internal static void Generate()
        {
            const int LimitSubclasses = 100;

            // MetaData columns
            var idColumn = _metadataTable.Table[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = _metadataTable.Table[DataConstants.MetaDataFieldColumn].Single();
            var dataSourceColumn = _metadataTable.Table[DataConstants.MetaDataSlkColumn].Single();
            var categoryColumn = _metadataTable.Table[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = _metadataTable.Table[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = _metadataTable.Table[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = _metadataTable.Table[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = _metadataTable.Table[DataConstants.MetaDataMaxValColumn].Single();
            var useHeroColumn = _metadataTable.Table[DataConstants.MetaDataUseHeroColumn].Single();
            var useUnitColumn = _metadataTable.Table[DataConstants.MetaDataUseUnitColumn].Single();
            var useBuildingColumn = _metadataTable.Table[DataConstants.MetaDataUseBuildingColumn].Single();

            var properties = _metadataTable.Table
                .Skip(1)
                .Where(property => property[useHeroColumn].ParseBool() || property[useUnitColumn].ParseBool() || property[useBuildingColumn].ParseBool())
                .Select(property => new PropertyModel
                {
                    Rawcode = (string)property[idColumn],
                    DataName = (string)property[fieldColumn],
                    DataSource = (string)property[dataSourceColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
                        (string)property[fieldColumn],
                        (string)property[categoryColumn],
                        (string)property[displayNameColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Specifics = ImmutableHashSet<int>.Empty,
                    SpecificUniqueNames = new(),
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);
            ObjectApiGenerator.PrecomputePropertyDataColumns(properties.Values, _dataTables);

            // Unit types (enum)
            ObjectApiGenerator.GenerateEnumFile(_enumModel);

            // Unit (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.UnitClassName, DataConstants.UnitTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.UnitClassName, DataConstants.UnitTypeEnumName, properties.Values));

#if false
            if (IsUnitClassAbstract)
            {
                const string testType = "unitSound";
                var testTypeValues = new HashSet<object>();

                var baseType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(DataConstants.UnitClassName));
                var baseTypeList = default(SeparatedSyntaxList<BaseTypeSyntax>);
                var baseList = SyntaxFactory.BaseList(baseTypeList.Add(baseType));

                var propertyRawcodes = properties
                    .Where(pair => pair.Value.Column != null)
                    .ToDictionary(model => model.Value.Name, model => model.Value.Rawcode);

                foreach (var unitType in data.Skip(1).Take(LimitSubclasses))
                {
                    var unitMembers = new List<MemberDeclarationSyntax>();
                    foreach (var pair in propertyRawcodes)
                    {
                        var property = pair.Key;
                        var type = properties[pair.Value].Type;
                        typeDict.TryGetValue(type, out var typeModel);
                        var propertyTypeName = typeModel?.Identifier ?? type /*throw new Exception(type)*/;
                        var propertyValue = unitType[(int)properties[pair.Value].Column];
                        if (string.Equals(type, testType, StringComparison.Ordinal))
                        {
                            testTypeValues.Add(propertyValue);
                        }

                        unitMembers.Add(SyntaxFactory.PropertyDeclaration(
                            default,
                            SyntaxFactory.TokenList(
                                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                                SyntaxFactory.Token(SyntaxKind.SealedKeyword),
                                SyntaxFactory.Token(SyntaxKind.OverrideKeyword)),
                            SyntaxFactory.ParseTypeName(propertyTypeName),
                            null,
                            SyntaxFactory.Identifier($"Default{properties[pair.Value].DisplayName.Dehumanize()}"),
                            null,
                            SyntaxFactory.ArrowExpressionClause(SyntaxFactory.ParseExpression(
                                // TODO: handle cases where default string is different (eg " - ")
                                propertyValue is null || (propertyValue is string propertyValueString && (string.IsNullOrEmpty(propertyValueString) || propertyValueString == "-" || propertyValueString == "_"))
                                ? "default"
                                : (typeModel?.ExpressionFunction ?? typeDict["string"].ExpressionFunction)(propertyValue) /*propertyValue.ToString()*/)),
                            null,
                            SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
                    }

                    var name = (((string)unitType[nameColumn]) ?? commentColumns.Where(col => (string)unitType[col] != null).Select(col => (string)unitType[col]).First()).Dehumanize();
                    var unitClass = SyntaxFactory.ClassDeclaration(
                        default,
                        new SyntaxTokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                            SyntaxFactory.Token(SyntaxKind.SealedKeyword)),
                        SyntaxFactory.Identifier(name),
                        null,
                        baseList,
                        default,
                        new SyntaxList<MemberDeclarationSyntax>(unitMembers));

                    classMembers.Add(unitClass);
                }

                Console.WriteLine();
                foreach (var value in testTypeValues)
                {
                    Console.WriteLine(value);
                }
            }
#endif

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.UnitClassName, IsUnitClassAbstract, DataConstants.BaseClassName, classMembers));

            // UnitLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                _enumModel.Members,
                properties.Values,
                _dataTables,
                DataConstants.UnitClassName,
                DataConstants.UnitTypeEnumName,
                IsUnitClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.UnitClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }
    }
}