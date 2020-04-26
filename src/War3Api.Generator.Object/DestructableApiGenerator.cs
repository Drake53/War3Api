// ------------------------------------------------------------------------------
// <copyright file="DestructableApiGenerator.cs" company="Drake53">
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
    internal static class DestructableApiGenerator
    {
        private const bool IsDestructableClassAbstract = false;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            var destructableData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.DestructableDataPath))).Shrink();
            var destructableMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.DestructableMetaDataPath))));

            Generate(destructableData, destructableMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            // Data columns
            var destructableIdColumn = data[DataConstants.DestructableDataKeyColumn].Single();
            var commentColumn = data[DataConstants.DestructableDataNameColumn].Single();

            // MetaData columns
            var idColumn = metaData[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = metaData[DataConstants.MetaDataFieldColumn].Single();
            var displayNameColumn = metaData[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = metaData[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = metaData[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = metaData[DataConstants.MetaDataMaxValColumn].Single();

            var properties = metaData
                .Skip(1)
                .Select(property => new PropertyModel()
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    DisplayName = ObjectApiGenerator.Localize((string)property[displayNameColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Column = data[property[fieldColumn]].Cast<int?>().SingleOrDefault(),
                })
                .ToDictionary(property => property.Rawcode);

            if (!IsDestructableClassAbstract)
            {
                var destructableTypeEnumModel = new EnumModel(DataConstants.DestructableTypeEnumName);
                foreach (var destructableType in data.Skip(1))
                {
                    var destructableTypeEnumMemberModel = new EnumMemberModel();

                    var name = ObjectApiGenerator.Localize((string)destructableType[commentColumn]);
                    destructableTypeEnumMemberModel.Name = name.Dehumanize();
                    destructableTypeEnumMemberModel.DisplayName = name;
                    destructableTypeEnumMemberModel.Value = ((string)destructableType[destructableIdColumn]).FromRawcode();

                    destructableTypeEnumModel.Members.Add(destructableTypeEnumMemberModel);
                }

                ObjectApiGenerator.GenerateEnumFile(destructableTypeEnumModel);
            }

            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.DestructableClassName, DataConstants.DestructableTypeEnumName, DataConstants.DestructableTypeEnumParameterName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.DestructableClassName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.DestructableClassName, IsDestructableClassAbstract, DataConstants.BaseClassName, classMembers));
        }
    }
}