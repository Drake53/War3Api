// ------------------------------------------------------------------------------
// <copyright file="DataTypeModel.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using War3Net.Build.Object;

namespace War3Api.Generator.Object.Models
{
    internal sealed class DataTypeModel
    {
        public ObjectDataType Type { get; set; }

        public ObjectDataType UnderlyingType { get; set; }

        // Name in .cs
        public string Identifier { get; set; }

        // Name of the property to retrieve this type's value from an ObjectDataModification.
        public string PropertyName { get; set; }
    }
}