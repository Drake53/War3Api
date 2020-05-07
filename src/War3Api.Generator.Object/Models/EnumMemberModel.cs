// ------------------------------------------------------------------------------
// <copyright file="EnumMemberModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Api.Generator.Object.Models
{
    internal sealed class EnumMemberModel
    {
        public string Name { get; set; }

        public string AlternativeName { get; set; }

        public string DisplayName { get; set; }

        public string UniqueName { get; set; }

        public int GameVersion { get; set; }

        public int Value { get; set; }

        public bool IsValueChar { get; set; }

        public string ValueString => IsValueChar ? $"'{(char)Value}'" : Value.ToString();
    }
}