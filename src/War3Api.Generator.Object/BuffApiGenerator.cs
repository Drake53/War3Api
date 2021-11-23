// ------------------------------------------------------------------------------
// <copyright file="BuffApiGenerator.cs" company="Drake53">
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
    internal static class BuffApiGenerator
    {
        private const bool IsBuffClassAbstract = false;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            var buffData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.BuffDataPath))).Shrink();
            var buffMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.BuffMetaDataPath))));

            Generate(buffData, buffMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            // Data columns
            var buffIdColumn = data[DataConstants.BuffDataKeyColumn].Single();
            var commentColumn = data[DataConstants.CommentsColumn].Single();

            // MetaData columns
            var idColumn = metaData[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = metaData[DataConstants.MetaDataFieldColumn].Single();
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
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Specifics = ImmutableHashSet<int>.Empty,
                    SpecificUniqueNames = new(),
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);

            // Buff types (enum)
            var buffTypeEnumModel = new EnumModel(DataConstants.BuffTypeEnumName);
            foreach (var buffType in data.Skip(1))
            {
                buffTypeEnumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel((string)buffType[commentColumn], (string)buffType[buffIdColumn]));
            }

            buffTypeEnumModel.EnsureMemberNamesUnique();

            ObjectApiGenerator.GenerateEnumFile(buffTypeEnumModel);

            // Buff (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.BuffClassName, DataConstants.BuffTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.BuffClassName, DataConstants.BuffTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.BuffClassName, IsBuffClassAbstract, DataConstants.BaseClassName, classMembers));

            // BuffLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                buffTypeEnumModel.Members,
                properties.Values,
                data,
                DataConstants.BuffClassName,
                DataConstants.BuffTypeEnumName,
                DataConstants.BuffDataKeyColumn,
                IsBuffClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.BuffClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }
    }
}