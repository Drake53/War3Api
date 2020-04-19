// ------------------------------------------------------------------------------
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

        // Unit
        internal const string UnitClassName = "Unit";
        internal const string UnitTypeEnumName = "UnitType";
        internal const string UnitTypeEnumParameterName = "baseUnitType";

        internal const string UnitDataKeyColumn = "unitID";
        internal const string UnitAbilityDataKeyColumn = "unitAbilID";
        internal const string UnitBalanceDataKeyColumn = "unitBalanceID";
        internal const string UnitUiDataKeyColumn = "unitUIID";
        internal const string UnitWeaponDataKeyColumn = "unitWeaponID";

        // Item
        internal const string ItemClassName = "Item";
        internal const string ItemTypeEnumName = "ItemType";
        internal const string ItemTypeEnumParameterName = "baseItemType";

        internal const string ItemDataKeyColumn = "itemID";

        // Destructable
        internal const string DestructableClassName = "Destructable";
        internal const string DestructableTypeEnumName = "DestructableType";
        internal const string DestructableTypeEnumParameterName = "baseDestructableType";

        internal const string DestructableDataKeyColumn = "DestructableID";

        // Doodad
        internal const string DoodadClassName = "Doodad";
        internal const string DoodadTypeEnumName = "DoodadType";
        internal const string DoodadTypeEnumParameterName = "baseDoodadType";

        internal const string DoodadDataKeyColumn = "doodID";

        // Ability
        internal const string AbilityClassName = "Ability";
        internal const string AbilityTypeEnumName = "AbilityType";
        internal const string AbilityTypeEnumParameterName = "baseAbilityType";

        internal const string AbilityDataKeyColumn = "alias";

        // Buff
        internal const string BuffClassName = "Buff";
        internal const string BuffTypeEnumName = "BuffType";
        internal const string BuffTypeEnumParameterName = "baseBuffType";

        internal const string BuffDataKeyColumn = "alias";

        // Upgrade
        internal const string UpgradeClassName = "Upgrade";
        internal const string UpgradeTypeEnumName = "UpgradeType";
        internal const string UpgradeTypeEnumParameterName = "baseUpgradeType";

        internal const string UpgradeDataKeyColumn = "upgradeid";

        // Metadata
        internal const string MetaDataIdColumn = "ID";
        internal const string MetaDataFieldColumn = "field";
        internal const string MetaDataRepeatColumn = "repeat";
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