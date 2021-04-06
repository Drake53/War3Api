using System.Collections;
using System.Collections.Generic;

namespace War3Api.Object
{
    internal static class InternalExtensions
    {
        internal static IEnumerable<TResult> CastWhere<TResult>(this IEnumerable source)
        {
            foreach (var item in source)
            {
                if (item is TResult result)
                {
                    yield return result;
                }
            }
        }
        
        internal static IEnumerable<TResult> CastWhere<TKey, TValue, TResult>(this Dictionary<TKey, TValue> source)
        {
            foreach (var pair in source)
            {
                if (pair.Value is TResult result)
                {
                    yield return result;
                }
            }
        }
    }
}