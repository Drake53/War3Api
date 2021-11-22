// ------------------------------------------------------------------------------
// <copyright file="EnumModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace War3Api.Generator.Object.Models
{
    internal sealed class EnumModel
    {
        public EnumModel(string name, bool unused = false)
        {
            Name = name;
            Unused = unused;
            Members = new List<EnumMemberModel>();
        }

        public string Name { get; set; }

        public bool Unused { get; set; }

        public IList<EnumMemberModel> Members { get; set; }

        public override string ToString() => Name;
    }
}