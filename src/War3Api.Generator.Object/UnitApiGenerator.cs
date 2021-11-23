// ------------------------------------------------------------------------------
// <copyright file="UnitApiGenerator.cs" company="Drake53">
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
    internal static class UnitApiGenerator
    {
        private const bool IsUnitClassAbstract = false;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            // The unit type Mercenarycampk ('nmrf') does not exist in unitdata.slk, so merge with UI data to generate it.
            var unitData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitDataPath)));
            var unitUiData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitUiDataPath)));

            if (IsUnitClassAbstract)
            {
                var unitAbilityData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitAbilityDataPath)));
                var unitBalanceData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitBalanceDataPath)));
                var unitWeaponData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitWeaponDataPath)));

                unitData = unitData.Combine(unitAbilityData, DataConstants.UnitDataKeyColumn, DataConstants.UnitAbilityDataKeyColumn);
                unitData = unitData.Combine(unitBalanceData, DataConstants.UnitDataKeyColumn, DataConstants.UnitBalanceDataKeyColumn);
                unitData = unitData.Combine(unitWeaponData, DataConstants.UnitDataKeyColumn, DataConstants.UnitWeaponDataKeyColumn);
            }

            unitData = unitData.Combine(unitUiData, DataConstants.UnitDataKeyColumn, DataConstants.UnitUiDataKeyColumn).Shrink();

            var unitMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitMetaDataPath))));

            Generate(unitData, unitMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            const int LimitSubclasses = 100;

            // Data columns
            var unitIdColumn = data[DataConstants.UnitDataKeyColumn].Single();
            var commentColumns = data[DataConstants.CommentOrCommentsColumn];
            var nameColumn = data[DataConstants.UnitDataNameColumn].Single();

            // MetaData columns
            var idColumn = metaData[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = metaData[DataConstants.MetaDataFieldColumn].Single();
            var categoryColumn = metaData[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = metaData[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = metaData[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = metaData[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = metaData[DataConstants.MetaDataMaxValColumn].Single();
            var useHeroColumn = metaData[DataConstants.MetaDataUseHeroColumn].Single();
            var useUnitColumn = metaData[DataConstants.MetaDataUseUnitColumn].Single();
            var useBuildingColumn = metaData[DataConstants.MetaDataUseBuildingColumn].Single();

            var properties = metaData
                .Skip(1)
                .Where(property => property[useHeroColumn].ParseBool() || property[useUnitColumn].ParseBool() || property[useBuildingColumn].ParseBool())
                .Select(property => new PropertyModel
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
                        (string)property[fieldColumn],
                        (string)property[categoryColumn],
                        (string)property[displayNameColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);

            // Unit types (enum)
            var unitTypeEnumModel = new EnumModel(DataConstants.UnitTypeEnumName);
            foreach (var unitType in data.Skip(1))
            {
                var name = ObjectApiGenerator.Localize(((string)unitType[nameColumn]) ?? commentColumns.Where(col => (string)unitType[col] != null).Select(col => (string)unitType[col]).First());

                unitTypeEnumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel(name, (string)unitType[unitIdColumn]));
            }

            unitTypeEnumModel.EnsureMemberNamesUnique();

            ObjectApiGenerator.GenerateEnumFile(unitTypeEnumModel);

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
                unitTypeEnumModel.Members,
                properties.Values,
                data,
                DataConstants.UnitClassName,
                DataConstants.UnitTypeEnumName,
                DataConstants.UnitDataKeyColumn,
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