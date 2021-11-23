using System;
using System.Text;

using Humanizer;

using Microsoft.CodeAnalysis.CSharp;

namespace War3Api.Generator.Object.Extensions
{
    public static class StringExtensions
    {
        public static string ToIdentifier(this string s)
        {
            s = s.Pascalize();

            var sb = new StringBuilder(s.Length + 1);

            var i = 0;
            while (i < s.Length)
            {
                var c = s[i++];
                if (SyntaxFacts.IsIdentifierStartCharacter(c))
                {
                    sb.Append(c);
                    break;
                }
                else if (SyntaxFacts.IsIdentifierPartCharacter(c))
                {
                    sb.Append('_');
                    sb.Append(c);
                    break;
                }
            }

            while (i < s.Length)
            {
                var c = s[i++];
                if (SyntaxFacts.IsIdentifierPartCharacter(c))
                {
                    sb.Append(c);
                }
                else if (c == '(')
                {
                    break;
                }
            }

            return sb.ToString();
        }

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