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
        private const bool IsAbilityClassAbstract = false;

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
            var displayNameColumn = metaData[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = metaData[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = metaData[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = metaData[DataConstants.MetaDataMaxValColumn].Single();
            var useSpecificColumn = metaData[DataConstants.MetaDataUseSpecificColumn].Single();

            var properties = metaData
                .Skip(1)
                .Where(property => string.IsNullOrEmpty((string)property[useSpecificColumn]))
                .Select(property => new PropertyModel()
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    Repeat = property[repeatColumn].ParseBool(),
                    DisplayName = ObjectApiGenerator.Localize((string)property[displayNameColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Column = data[property[fieldColumn]].Cast<int?>().SingleOrDefault(),
                })
                .ToDictionary(property => property.Rawcode);

            if (!IsAbilityClassAbstract)
            {
                var abilityTypeEnumModel = new EnumModel(DataConstants.AbilityTypeEnumName);
                foreach (var abilityType in data.Skip(1))
                {
                    if (string.IsNullOrEmpty((string)abilityType[abilityIdColumn]))
                    {
                        continue;
                    }

                    var abilityTypeEnumMemberModel = new EnumMemberModel();

                    var name = ObjectApiGenerator.Localize((string)abilityType[commentColumn]);
                    abilityTypeEnumMemberModel.Name = name.Dehumanize();
                    abilityTypeEnumMemberModel.DisplayName = name;
                    abilityTypeEnumMemberModel.Value = ((string)abilityType[abilityIdColumn]).FromRawcode();

                    abilityTypeEnumModel.Members.Add(abilityTypeEnumMemberModel);
                }

                ObjectApiGenerator.GenerateEnumFile(abilityTypeEnumModel);
            }

            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.AbilityClassName, DataConstants.AbilityTypeEnumName, DataConstants.AbilityTypeEnumParameterName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.AbilityClassName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.AbilityClassName, IsAbilityClassAbstract, DataConstants.BaseClassName, classMembers));
        }
    }
}