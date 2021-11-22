using System;
using System.Linq;

using War3Api.Generator.Object.Models;

using War3Net.Common.Extensions;

namespace War3Api.Generator.Object.Extensions
{
    internal static class EnumModelExtensions
    {
        internal static void EnsureMemberNamesUnique(this EnumModel enumModel)
        {
            var duplicateNames = enumModel.Members
                .GroupBy(member => member.Name, StringComparer.Ordinal)
                .Where(grouping => grouping.Count() > 1)
                .Select(grouping => grouping.Key)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var member in enumModel.Members)
            {
                member.UniqueName = duplicateNames.Contains(member.Name)
                    ? $"{member.Name}_{member.Value.ToRawcode()}"
                    : member.Name;
            }
        }
    }
}