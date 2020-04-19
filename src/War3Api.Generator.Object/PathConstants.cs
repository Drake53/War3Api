// ------------------------------------------------------------------------------
// <copyright file="PathConstants.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Api.Generator.Object
{
    internal static class PathConstants
    {
        // Base
        internal const string WorldEditStringsPath = @"_locales\enus.w3mod\ui\worldeditstrings.txt";
        internal const string EnumDataFilePath = @"ui\uniteditordata.txt";

        // Unit
        internal const string UnitDataPath = @"units\unitdata.slk";
        internal const string UnitAbilityDataPath = @"units\unitabilities.slk";
        internal const string UnitBalanceDataPath = @"units\unitbalance.slk";
        internal const string UnitUiDataPath = @"units\unitui.slk";
        internal const string UnitWeaponDataPath = @"units\unitweapons.slk";

        // Item
        internal const string ItemDataPath = @"units\itemdata.slk";

        // Destructable
        internal const string DestructableDataPath = @"units\destructabledata.slk";

        // Doodad
        internal const string DoodadDataPath = @"doodads\doodads.slk";

        // Ability
        internal const string AbilityDataPath = @"units\abilitydata.slk";

        // Buff
        internal const string BuffDataPath = @"units\abilitybuffdata.slk";

        // Upgrade
        internal const string UpgradeDataPath = @"units\upgradedata.slk";

        // Metadata
        internal const string UnitMetaDataPath = @"units\unitmetadata.slk";
        internal const string ItemMetaDataPath = UnitMetaDataPath;
        internal const string DestructableMetaDataPath = @"units\destructablemetadata.slk";
        internal const string DoodadMetaDataPath = @"doodads\doodadmetadata.slk";
        internal const string AbilityMetaDataPath = @"units\abilitymetadata.slk";
        internal const string BuffMetaDataPath = @"units\abilitybuffmetadata.slk";
        internal const string UpgradeMetaDataPath = @"units\upgrademetadata.slk";
    }
}