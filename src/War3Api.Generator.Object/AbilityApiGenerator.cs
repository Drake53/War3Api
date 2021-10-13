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

using Humanizer;

using Microsoft.CodeAnalysis.CSharp.Syntax;

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
                .Select(property => new PropertyModel()
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    Repeat = property[repeatColumn].ParseBool(),
                    Data = (int)property[dataColumn],
                    Category = ObjectApiGenerator.Localize(ObjectApiGenerator.LookupCategory((string)property[categoryColumn])),
                    DisplayName = ObjectApiGenerator.Localize((string)property[displayNameColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    UseSpecific = (string)property[useSpecificColumn],
                    Column = data[property[fieldColumn]].Cast<int?>().SingleOrDefault(),
                })
                .ToDictionary(property => property.Rawcode);

            foreach (var propertyModel in properties.Values)
            {
                var category = propertyModel.Category.Replace("&", string.Empty, StringComparison.Ordinal).Dehumanize();
                var name = new string(propertyModel.DisplayName.Where(@char => @char != '(' && @char != ')').ToArray()).Dehumanize();

                propertyModel.DehumanizedName = category + name;
            }

            // Ability types (enum)
            var abilityTypeEnumModel = new EnumModel(DataConstants.AbilityTypeEnumName);
            foreach (var abilityType in data.Skip(1))
            {
                if (string.IsNullOrEmpty((string)abilityType[abilityIdColumn]))
                {
                    continue;
                }

                var abilityTypeEnumMemberModel = new EnumMemberModel();

                var name = ObjectApiGenerator.Localize((string)abilityType[commentColumn]);
                abilityTypeEnumMemberModel.Name = name.Dehumanize().TrimStart('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                abilityTypeEnumMemberModel.DisplayName = name;
                abilityTypeEnumMemberModel.Value = ((string)abilityType[abilityIdColumn]).FromRawcode();

                abilityTypeEnumModel.Members.Add(abilityTypeEnumMemberModel);
            }

            var duplicateNames = abilityTypeEnumModel.Members.GroupBy(member => member.Name).Where(grouping => grouping.Count() > 1).Select(grouping => grouping.Key).ToHashSet();
            foreach (var member in abilityTypeEnumModel.Members)
            {
                member.UniqueName = duplicateNames.Contains(member.Name) ? $"{member.Name}_{member.Value.ToRawcode()}" : member.Name;
            }

            // if (!IsAbilityClassAbstract)
            {
                ObjectApiGenerator.GenerateEnumFile(abilityTypeEnumModel);
            }

            // Ability (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.AbilityClassName, properties.Values.Where(property => string.IsNullOrEmpty(property.UseSpecific)), IsAbilityClassAbstract));

            if (!IsAbilityClassAbstract)
            {
                classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.AbilityClassName, DataConstants.AbilityTypeEnumName, DataConstants.AbilityTypeEnumParameterName));
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
                                properties.Values.Where(property => property.UseSpecific?.Contains(abilityType.Value.ToRawcode(), StringComparison.Ordinal) ?? false),
                                false,
                                false,
                                abilityType.Value)),
                        DataConstants.AbilityNamespace);
                }
            }
        }
    }
}