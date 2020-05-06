// ------------------------------------------------------------------------------
// <copyright file="PropertyModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Api.Generator.Object.Models
{
    internal sealed class PropertyModel
    {
        public string Rawcode { get; set; }

        // Column name
        public string Name { get; set; }

        // If true, property's value can be set for each level (or variation for doodads) individually.
        public bool Repeat { get; set; }

        public string Category { get; set; }

        public string DisplayName { get; set; }

        public string DehumanizedName { get; set; }

        public string Type { get; set; }

        public object MinVal { get; set; }

        public object MaxVal { get; set; }

        public string UseSpecific { get; set; }

        public int? Column { get; set; }
    }
}