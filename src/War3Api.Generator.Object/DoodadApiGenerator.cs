// ------------------------------------------------------------------------------
// <copyright file="DoodadApiGenerator.cs" company="Drake53">
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
    internal static class DoodadApiGenerator
    {
        private const bool IsDoodadClassAbstract = false;

        internal static void Generate(string inputFolder)
        {
            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            var doodadData = new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.DoodadDataPath))).Shrink();
            var doodadMetaData = ObjectApiGenerator.Localize(new SylkParser().Parse(File.OpenRead(Path.Combine(inputFolder, PathConstants.DoodadMetaDataPath))));

            Generate(doodadData, doodadMetaData);
        }

        internal static void Generate(SylkTable data, SylkTable metaData)
        {
            // Data columns
            var doodadIdColumn = data[DataConstants.DoodadDataKeyColumn].Single();
            var commentColumn = data[DataConstants.CommentColumn].Single();

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
                .Select(property => new PropertyModel()
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    Repeat = property[repeatColumn].ParseBool(),
                    Category = ObjectApiGenerator.Localize(ObjectApiGenerator.LookupCategory((string)property[categoryColumn])),
                    DisplayName = ObjectApiGenerator.Localize((string)property[displayNameColumn]),
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

            if (!IsDoodadClassAbstract)
            {
                var doodadTypeEnumModel = new EnumModel(DataConstants.DoodadTypeEnumName);
                foreach (var doodadType in data.Skip(1))
                {
                    var doodadTypeEnumMemberModel = new EnumMemberModel();

                    var name = ObjectApiGenerator.Localize((string)doodadType[commentColumn]);
                    doodadTypeEnumMemberModel.Name = name.Dehumanize();
                    doodadTypeEnumMemberModel.DisplayName = name;
                    doodadTypeEnumMemberModel.Value = ((string)doodadType[doodadIdColumn]).FromRawcode();

                    doodadTypeEnumModel.Members.Add(doodadTypeEnumMemberModel);
                }

                ObjectApiGenerator.GenerateEnumFile(doodadTypeEnumModel);
            }

            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.DoodadClassName, DataConstants.DoodadTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.DoodadClassName, DataConstants.DoodadTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.DoodadClassName, IsDoodadClassAbstract, DataConstants.BaseClassName, classMembers));
        }
    }
}