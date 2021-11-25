// ------------------------------------------------------------------------------
// <copyright file="PropertyModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Immutable;

namespace War3Api.Generator.Object.Models
{
    internal sealed class PropertyModel
    {
        public string Rawcode { get; set; }

        // Column name
        public string DataName { get; set; }

        // Slk name
        public string DataSource { get; set; }

        public int DataColumn { get; set; }

        public int[] DataColumns { get; set; }

        public string IdentifierName { get; set; }

        public string UniqueName { get; set; }

        // The amount of levels or variations.
        public int Repeat { get; set; }

        public int Data { get; set; }

        public string Type { get; set; }

        public object MinVal { get; set; }

        public object MaxVal { get; set; }

        public ImmutableHashSet<int> Specifics { get; set; }

        public Dictionary<int, string> SpecificUniqueNames { get; set; }

        public override string ToString() => UniqueName;
    }
}