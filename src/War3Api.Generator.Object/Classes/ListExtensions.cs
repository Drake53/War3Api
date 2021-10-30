using System.Collections.Generic;
using War3Net.Build.Object;

namespace War3Api.Object
{
    public static class ListExtensions
    {
        /// <summary>
        /// Convert into a SimpleObjectDataModifications, copying over all data.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static SimpleObjectDataModifications ToObjectDataModifications(this List<SimpleObjectDataModification> list)
        {
            SimpleObjectDataModifications simpleObjectDataModifications = new();
            foreach (var mod in list)
            {
                simpleObjectDataModifications[mod.Id] = mod;
            }
            return simpleObjectDataModifications;
        }

        /// <summary>
        /// Convert into a LevelObjectDataModifications, copying over all data.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static LevelObjectDataModifications ToObjectDataModifications(this List<LevelObjectDataModification> list)
        {
            LevelObjectDataModifications levelObjectDataModifications = new();
            foreach (var mod in list)
            {
                levelObjectDataModifications[mod.Id] = mod;
            }
            return levelObjectDataModifications;
        }

        /// <summary>
        /// Convert into a VariationObjectDataModifications, copying over all data.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static VariationObjectDataModifications ToObjectDataModifications(this List<VariationObjectDataModification> list)
        {
            VariationObjectDataModifications variationObjectDataModifications = new();
            foreach (var mod in list)
            {
                variationObjectDataModifications[mod.Id] = mod;
            }
            return variationObjectDataModifications;
        }
    }
}