using System.Collections.Generic;
using System.Linq;

using War3Net.Build.Object;

namespace War3Api.Object.Tests
{
    internal static class Extensions
    {
        public static IEnumerable<SimpleObjectModification> GetAllUnits(this ObjectData objectData)
        {
            return objectData.UnitData.BaseUnits.Concat(objectData.UnitData.NewUnits);
        }

        public static IEnumerable<SimpleObjectModification> GetAllItems(this ObjectData objectData)
        {
            return objectData.ItemData.BaseItems.Concat(objectData.ItemData.NewItems);
        }

        public static IEnumerable<SimpleObjectModification> GetAllDestructables(this ObjectData objectData)
        {
            return objectData.DestructableData.BaseDestructables.Concat(objectData.DestructableData.NewDestructables);
        }

        public static IEnumerable<VariationObjectModification> GetAllDoodads(this ObjectData objectData)
        {
            return objectData.DoodadData.BaseDoodads.Concat(objectData.DoodadData.NewDoodads);
        }

        public static IEnumerable<LevelObjectModification> GetAllAbilities(this ObjectData objectData)
        {
            return objectData.AbilityData.BaseAbilities.Concat(objectData.AbilityData.NewAbilities);
        }

        public static IEnumerable<SimpleObjectModification> GetAllBuffs(this ObjectData objectData)
        {
            return objectData.BuffData.BaseBuffs.Concat(objectData.BuffData.NewBuffs);
        }

        public static IEnumerable<LevelObjectModification> GetAllUpgrades(this ObjectData objectData)
        {
            return objectData.UpgradeData.BaseUpgrades.Concat(objectData.UpgradeData.NewUpgrades);
        }

        public static IEnumerable<SimpleObjectDataModification> GetAllUnitModifications(this ObjectData objectData)
        {
            return objectData.UnitData.BaseUnits.Concat(objectData.UnitData.NewUnits).SelectMany(unit => unit.Modifications);
        }

        public static IEnumerable<SimpleObjectDataModification> GetAllItemModifications(this ObjectData objectData)
        {
            return objectData.ItemData.BaseItems.Concat(objectData.ItemData.NewItems).SelectMany(item => item.Modifications);
        }

        public static IEnumerable<SimpleObjectDataModification> GetAllDestructableModifications(this ObjectData objectData)
        {
            return objectData.DestructableData.BaseDestructables.Concat(objectData.DestructableData.NewDestructables).SelectMany(destructable => destructable.Modifications);
        }

        public static IEnumerable<VariationObjectDataModification> GetAllDoodadModifications(this ObjectData objectData)
        {
            return objectData.DoodadData.BaseDoodads.Concat(objectData.DoodadData.NewDoodads).SelectMany(doodad => doodad.Modifications);
        }

        public static IEnumerable<LevelObjectDataModification> GetAllAbilityModifications(this ObjectData objectData)
        {
            return objectData.AbilityData.BaseAbilities.Concat(objectData.AbilityData.NewAbilities).SelectMany(ability => ability.Modifications);
        }

        public static IEnumerable<SimpleObjectDataModification> GetAllBuffModifications(this ObjectData objectData)
        {
            return objectData.BuffData.BaseBuffs.Concat(objectData.BuffData.NewBuffs).SelectMany(buff => buff.Modifications);
        }

        public static IEnumerable<LevelObjectDataModification> GetAllUpgradeModifications(this ObjectData objectData)
        {
            return objectData.UpgradeData.BaseUpgrades.Concat(objectData.UpgradeData.NewUpgrades).SelectMany(upgrade => upgrade.Modifications);
        }

        public static IEnumerable<ObjectDataModification> GetAllModifications(this ObjectData objectData)
        {
            return objectData.GetAllUnitModifications().Cast<ObjectDataModification>()
                .Concat(objectData.GetAllItemModifications())
                .Concat(objectData.GetAllDestructableModifications())
                .Concat(objectData.GetAllDoodadModifications())
                .Concat(objectData.GetAllAbilityModifications())
                .Concat(objectData.GetAllBuffModifications())
                .Concat(objectData.GetAllUpgradeModifications());
        }
    }
}