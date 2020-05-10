// ------------------------------------------------------------------------------
// <copyright file="TypeModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using War3Net.Build.Object;

namespace War3Api.Generator.Object.Models
{
    internal sealed class TypeModel
    {
        // Name in .slk
        public string Name { get; set; }

        public ObjectDataType Type { get; set; }

        public TypeModelCategory Category { get; set; }

        // For lists, this is the generic type identifier.
        public string Identifier { get; set; }

        // Name in .cs
        public string FullIdentifier => Category == TypeModelCategory.List ? $"{ObjectApiGenerator.ListTypeName}<{Identifier}>" : Identifier;
    }
}