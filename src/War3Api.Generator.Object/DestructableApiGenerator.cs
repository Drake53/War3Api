// ------------------------------------------------------------------------------
// <copyright file="DestructableApiGenerator.cs" company="Drake53">
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
    internal static class DestructableApiGenerator
    {
        private const bool IsDestructableClassAbstract = false;

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
                new TableModel(Path.Combine(inputFolder, PathConstants.DestructableDataPath), DataConstants.DestructableDataKeyColumn, DataConstants.DestructableDataNameColumn),
            }
            .ToDictionary(table => table.TableName, StringComparer.OrdinalIgnoreCase);

            _metadataTable = new TableModel(Path.Combine(inputFolder, PathConstants.DestructableMetaDataPath));
            ObjectApiGenerator.Localize(_metadataTable.Table);

            _enumModel = new EnumModel(DataConstants.DestructableTypeEnumName);

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
                    DataName = (string)property[fieldColumn],
                    DataSource = (string)property[dataSourceColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
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
            ObjectApiGenerator.PrecomputePropertyDataColumns(properties.Values, _dataTables);

            // Destructable types (enum)
            ObjectApiGenerator.GenerateEnumFile(_enumModel);

            // Destructable (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.DestructableClassName, DataConstants.DestructableTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.DestructableClassName, DataConstants.DestructableTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.DestructableClassName, IsDestructableClassAbstract, DataConstants.BaseClassName, classMembers));

            // DestructableLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                _enumModel.Members,
                properties.Values,
                _dataTables,
                DataConstants.DestructableClassName,
                DataConstants.DestructableTypeEnumName,
                IsDestructableClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.DestructableClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }
    }
}