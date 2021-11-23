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

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
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
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);

            // Upgrade types (enum)
            var upgradeTypeEnumModel = new EnumModel(DataConstants.UpgradeTypeEnumName);
            foreach (var upgradeType in data.Skip(1))
            {
                upgradeTypeEnumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel((string)upgradeType[commentColumn], (string)upgradeType[upgradeIdColumn]));
            }

            upgradeTypeEnumModel.EnsureMemberNamesUnique();

            ObjectApiGenerator.GenerateEnumFile(upgradeTypeEnumModel);

            // Upgrade (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.UpgradeClassName, DataConstants.UpgradeTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.UpgradeClassName, DataConstants.UpgradeTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.UpgradeClassName, IsUpgradeClassAbstract, DataConstants.BaseClassName, classMembers));

            // UpgradeLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                upgradeTypeEnumModel.Members,
                properties.Values,
                data,
                DataConstants.UpgradeClassName,
                DataConstants.UpgradeTypeEnumName,
                DataConstants.UpgradeDataKeyColumn,
                IsUpgradeClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.UpgradeClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }
    }
}