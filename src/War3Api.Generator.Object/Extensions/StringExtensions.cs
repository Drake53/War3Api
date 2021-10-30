using System;

using Humanizer;

namespace War3Api.Generator.Object.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string s, bool isPascalCase = false, bool prefixUnderscore = false)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (!isPascalCase)
            {
                s = s.Dehumanize();
            }

            s = s.Length < 2
                ? s.ToLowerInvariant()
                : char.ToLowerInvariant(s[0]) + s[1..];

            if (prefixUnderscore)
            {
                s = '_' + s;
            }

            return s;
        }
    }
}