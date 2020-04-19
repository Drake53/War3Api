// ------------------------------------------------------------------------------
// <copyright file="TypeModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;

using War3Net.Build.Object;

namespace War3Api.Generator.Object.Models
{
    internal sealed class TypeModel
    {
        // Name in .slk
        public string Name { get; set; }

        public ObjectDataType Type { get; set; }

        public bool IsBasicType { get; set; }

        // Name in .cs
        public string Identifier { get; set; }

        public Func<object, string> ExpressionFunction { get; set; }
    }
}