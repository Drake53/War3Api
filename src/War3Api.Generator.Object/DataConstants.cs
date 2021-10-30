﻿// ------------------------------------------------------------------------------
// <copyright file="DataConstants.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Api.Generator.Object
{
    internal static class DataConstants
    {
        // Base
        internal const string BaseClassName = "BaseObject";

        internal const string LevelDictClassName = "LevelObjectDataModifications";
        internal const string SimpleDictClassName = "SimpleObjectDataModifications";
        internal const string VariationDictClassName = "VariationObjectDataModifications";

        internal const string CommentColumn = "comment";
        internal const string CommentsColumn = "comments";
        internal const string CommentOrCommentsColumn = "comment(s)";

        // Unit
        internal const string UnitNamespace = "Units";
        internal const string UnitClassName = "Unit";
        internal const string UnitTypeEnumName = "UnitType";
        internal const string UnitTypeEnumParameterName = "baseUnitType";

        internal const string UnitDataKeyColumn = "unitID";
        internal const string UnitAbilityDataKeyColumn = "unitAbilID";
        internal const string UnitBalanceDataKeyColumn = "unitBalanceID";
        internal const string UnitUiDataKeyColumn = "unitUIID";
        internal const string UnitWeaponDataKeyColumn = "unitWeaponID";
        internal const string UnitDataNameColumn = "name";

        // Item
        internal const string ItemNamespace = "Items";
        internal const string ItemClassName = "Item";
        internal const string ItemTypeEnumName = "ItemType";

        internal const string ItemDataKeyColumn = "itemID";

        // Destructable
        internal const string DestructableNamespace = "Destructables";
        internal const string DestructableClassName = "Destructable";
        internal const string DestructableTypeEnumName = "DestructableType";

        internal const string DestructableDataKeyColumn = "DestructableID";
        internal const string DestructableDataNameColumn = "Name";

        // Doodad
        internal const string DoodadNamespace = "Doodads";
        internal const string DoodadClassName = "Doodad";
        internal const string DoodadTypeEnumName = "DoodadType";

        internal const string DoodadDataKeyColumn = "doodID";

        // Ability
        internal const string AbilityNamespace = "Abilities";
        internal const string AbilityClassName = "Ability";
        internal const string AbilityTypeEnumName = "AbilityType";

        internal const string AbilityDataKeyColumn = "alias";

        // Buff
        internal const string BuffNamespace = "Buffs";
        internal const string BuffClassName = "Buff";
        internal const string BuffTypeEnumName = "BuffType";

        internal const string BuffDataKeyColumn = "alias";

        // Upgrade
        internal const string UpgradeNamespace = "Upgrades";
        internal const string UpgradeClassName = "Upgrade";
        internal const string UpgradeTypeEnumName = "UpgradeType";

        internal const string UpgradeDataKeyColumn = "upgradeid";

        // Metadata
        /// <summary>The property's rawcode.</summary>
        internal const string MetaDataIdColumn = "ID";
        /// <summary>The property's column name in the corresponding object data .slk file.</summary>
        internal const string MetaDataFieldColumn = "field";
        /// <summary>If true (value is not 0), this property's value can be set individually for every level or variation of the object.</summary>
        internal const string MetaDataRepeatColumn = "repeat";
        internal const string MetaDataDataColumn = "data";
        internal const string MetaDataCategoryColumn = "category";
        internal const string MetaDataDisplayNameColumn = "displayName";
        internal const string MetaDataSortColumn = "sort";
        internal const string MetaDataTypeColumn = "type";
        internal const string MetaDataMinValColumn = "minVal";
        internal const string MetaDataMaxValColumn = "maxVal";
        internal const string MetaDataUseHeroColumn = "useHero";
        internal const string MetaDataUseUnitColumn = "useUnit";
        internal const string MetaDataUseBuildingColumn = "useBuilding";
        internal const string MetaDataUseItemColumn = "useItem";
        internal const string MetaDataUseCreepColumn = "useCreep";
        internal const string MetaDataUseSpecificColumn = "useSpecific";
    }
}