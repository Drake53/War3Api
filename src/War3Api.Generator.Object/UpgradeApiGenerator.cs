// ------------------------------------------------------------------------------
// <copyright file="UpgradeApiGenerator.cs" company="Drake53">
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
    internal static class UpgradeApiGenerator
    {
        private const bool IsUpgradeClassAbstract = false;

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
                new TableModel(Path.Combine(inputFolder, PathConstants.UpgradeDataPath), DataConstants.UpgradeDataKeyColumn, DataConstants.CommentsColumn),
            }
            .ToDictionary(table => table.TableName, StringComparer.OrdinalIgnoreCase);

            _metadataTable = new TableModel(Path.Combine(inputFolder, PathConstants.UpgradeMetaDataPath));
            ObjectApiGenerator.Localize(_metadataTable.Table);

            _enumModel = new EnumModel(DataConstants.UpgradeTypeEnumName);

            var members = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var dataTable in _dataTables.Values)
            {
                dataTable.AddValues(members);
            }

            foreach (var member in members)
            {
                _enumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel(member.Value, member.Key));
            }

            _enumModel.EnsureMemberNamesUnique();

            _initialized = true;
        }

        internal static void Generate()
        {
            // MetaData columns
            var idColumn = _metadataTable.Table[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = _metadataTable.Table[DataConstants.MetaDataFieldColumn].Single();
            var dataSourceColumn = _metadataTable.Table[DataConstants.MetaDataSlkColumn].Single();
            var repeatColumn = _metadataTable.Table[DataConstants.MetaDataRepeatColumn].Single();
            var categoryColumn = _metadataTable.Table[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = _metadataTable.Table[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = _metadataTable.Table[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = _metadataTable.Table[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = _metadataTable.Table[DataConstants.MetaDataMaxValColumn].Single();

            var properties = _metadataTable.Table
                .Skip(1)
                .Select(property => new PropertyModel
                {
                    Rawcode = (string)property[idColumn],
                    Name = (string)property[fieldColumn],
                    DataSource = (string)property[dataSourceColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
                        (string)property[fieldColumn],
                        (string)property[categoryColumn],
                        (string)property[displayNameColumn]),
                    Repeat = (int)property[repeatColumn],
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Specifics = ImmutableHashSet<int>.Empty,
                    SpecificUniqueNames = new(),
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);

            // Upgrade types (enum)
            ObjectApiGenerator.GenerateEnumFile(_enumModel);

            // Upgrade (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.UpgradeClassName, DataConstants.UpgradeTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.UpgradeClassName, DataConstants.UpgradeTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.UpgradeClassName, IsUpgradeClassAbstract, DataConstants.BaseClassName, classMembers));

            // UpgradeLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                _enumModel.Members,
                properties.Values,
                _dataTables,
                DataConstants.UpgradeClassName,
                DataConstants.UpgradeTypeEnumName,
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