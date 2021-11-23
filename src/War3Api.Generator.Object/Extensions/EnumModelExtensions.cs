using System;

using War3Api.Generator.Object.Models;

using War3Net.Common.Extensions;

namespace War3Api.Generator.Object.Extensions
{
    internal static class EnumModelExtensions
    {
        internal static void EnsureMemberNamesUnique(this EnumModel enumModel)
        {
            var duplicateNames = enumModel.Members.FindDuplicates(member => member.Name, StringComparer.Ordinal);

            foreach (var member in enumModel.Members)
            {
                member.UniqueName = duplicateNames.Contains(member.Name)
                    ? $"{member.Name}_{member.Value.ToRawcode()}"
                    : member.Name;
            }
        }
    }
}