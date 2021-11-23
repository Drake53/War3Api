using System;
using System.Collections.Generic;
using System.Linq;

namespace War3Api.Generator.Object.Extensions
{
    public static class IEnumerableExtensions
    {
        public static HashSet<TKey> FindDuplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        {
            return source
                .GroupBy(keySelector, comparer)
                .Where(grouping => grouping.Take(2).Count() > 1)
                .Select(grouping => grouping.Key)
                .ToHashSet(comparer);
        }
    }
}