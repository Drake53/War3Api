﻿// ------------------------------------------------------------------------------
// <copyright file="ItemApiGenerator.cs" company="Drake53">
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

using Humanizer;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
using War3Api.Generator.Object.Models;

using War3Net.Common.Extensions;
using War3Net.IO.Slk;

namespace War3Api.Generator.Object
{
    internal static class ItemApiGenerator
    {
        private const bool IsItemClassAbstract = false;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            var itemData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.ItemDataPath))).Shrink();
            var unitMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.UnitMetaDataPath))));

            Generate(itemData, unitMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            const int LimitSubclasses = 100;

            // Data columns
            var itemIdColumn = data[DataConstants.ItemDataKeyColumn].Single();
            var commentColumn = data[DataConstants.CommentColumn].Single();

            // MetaData columns
            var idColumn = metaData[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = metaData[DataConstants.MetaDataFieldColumn].Single();
            var categoryColumn = metaData[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = metaData[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = metaData[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = metaData[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = metaData[DataConstants.MetaDataMaxValColumn].Single();
            var useItemColumn = metaData[DataConstants.MetaDataUseItemColumn].Single();

            var properties = metaData
                .Skip(1)
                .Where(property => property[useItemColumn].ParseBool())
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
                    Specifics = ImmutableHashSet<int>.Empty,
                    SpecificUniqueNames = new(),
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);

            // Item types (enum)
            var itemTypeEnumModel = new EnumModel(DataConstants.ItemTypeEnumName);
            foreach (var itemType in data.Skip(1))
            {
                itemTypeEnumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel((string)itemType[commentColumn], (string)itemType[itemIdColumn]));
            }

            itemTypeEnumModel.EnsureMemberNamesUnique();

            ObjectApiGenerator.GenerateEnumFile(itemTypeEnumModel);

            // Item (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.ItemClassName, DataConstants.ItemTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.ItemClassName, DataConstants.ItemTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.ItemClassName, IsItemClassAbstract, DataConstants.BaseClassName, classMembers));

            // ItemLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                itemTypeEnumModel.Members,
                properties.Values,
                data,
                DataConstants.ItemClassName,
                DataConstants.ItemTypeEnumName,
                DataConstants.ItemDataKeyColumn,
                IsItemClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.ItemClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }
    }
}