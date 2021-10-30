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
        public static SimpleObjectDataModifications ToSimpleObjectDataModifications(this List<SimpleObjectDataModification> list)
        {
            SimpleObjectDataModifications simpleObjectDataModifications = new();
            foreach (var mod in list)
            {
                simpleObjectDataModifications[mod.Id] = mod;
            }
            return simpleObjectDataModifications;
        }
    }
}