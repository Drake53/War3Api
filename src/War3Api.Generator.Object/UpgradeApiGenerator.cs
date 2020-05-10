// ------------------------------------------------------------------------------
// <copyright file="UpgradeApiGenerator.cs" company="Drake53">
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
    internal static class UpgradeApiGenerator
    {
        private const bool IsUpgradeClassAbstract = false;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            var upgradeData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UpgradeDataPath))).Shrink();
            var upgradeMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UpgradeMetaDataPath))));

            Generate(upgradeData, upgradeMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            // Data columns
            var upgradeIdColumn = data[DataConstants.UpgradeDataKeyColumn].Single();
            var commentColumn = data[DataConstants.CommentsColumn].Single();

            // MetaData columns
            var idColumn = metaData[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = metaData[DataConstants.MetaDataFieldColumn].Single();
            var repeatColumn = metaData[DataConstants.MetaDataRepeatColumn].Single();
            var categoryColumn = metaData[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = metaData[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = metaData[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = metaData[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = metaData[DataConstants.MetaDataMaxValColumn].Single();

            static string GetDisplayName(string displayNameColumn, string fieldColumn)
            {
                var localized = ObjectApiGenerator.Localize(displayNameColumn);
                return localized.Contains('%', StringComparison.Ordinal) ? fieldColumn : localized;
            }

            var properties = metaData
                .Skip(1)
                .Select(property => new PropertyModel()
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    Repeat = property[repeatColumn].ParseBool(),
                    Category = ObjectApiGenerator.Localize(ObjectApiGenerator.LookupCategory((string)property[categoryColumn])),
                    DisplayName = GetDisplayName((string)property[displayNameColumn], (string)property[fieldColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Column = data[property[fieldColumn]].Cast<int?>().SingleOrDefault(),
                })
                .ToDictionary(property => property.Rawcode);

            foreach (var propertyModel in properties.Values)
            {
                var category = propertyModel.Category.Replace("&", string.Empty, StringComparison.Ordinal).Dehumanize();
                var name = new string(propertyModel.DisplayName.Where(@char => @char != '(' && @char != ')').ToArray()).Dehumanize();

                propertyModel.DehumanizedName = category + name;
            }

            if (!IsUpgradeClassAbstract)
            {
                var upgradeTypeEnumModel = new EnumModel(DataConstants.UpgradeTypeEnumName);
                foreach (var upgradeType in data.Skip(1))
                {
                    var upgradeTypeEnumMemberModel = new EnumMemberModel();

                    var name = ObjectApiGenerator.Localize((string)upgradeType[commentColumn]);
                    upgradeTypeEnumMemberModel.Name = name.Dehumanize();
                    upgradeTypeEnumMemberModel.DisplayName = name;
                    upgradeTypeEnumMemberModel.Value = ((string)upgradeType[upgradeIdColumn]).FromRawcode();

                    upgradeTypeEnumModel.Members.Add(upgradeTypeEnumMemberModel);
                }

                ObjectApiGenerator.GenerateEnumFile(upgradeTypeEnumModel);
            }

            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.UpgradeClassName, DataConstants.UpgradeTypeEnumName, DataConstants.UpgradeTypeEnumParameterName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.UpgradeClassName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.UpgradeClassName, IsUpgradeClassAbstract, DataConstants.BaseClassName, classMembers));
        }
    }
}