// ------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Net.CodeAnalysis.Common;
using War3Net.CodeAnalysis.Jass;

namespace War3Api.Generator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // const string LatestVersion = "1.31.1";
            const string LatestVersion = "1.32.0.4-beta";

            // War3Net.CodeAnalysis.CSharp.CompilationHelper.SerializeTo();
            if (args.Length > 0)
            {
                GetReferencesAndUsingDirectives(
                    LatestVersion,
                    args.Length > 0 ? args[0] : null,
                    args.Length > 1 ? args[1] : null);
            }
            else
            {
                GetReferencesAndUsingDirectives(
                    LatestVersion,
                    $@"..\..\..\..\War3Api.Common\Generated\{LatestVersion}\Common.cs",
                    $@"..\..\..\..\War3Api.Blizzard\Generated\{LatestVersion}\Blizzard.cs");
            }
        }

        // TODO: don't copypaste everything from War3Net.CodeAnalysis.Jass.JassTranspiler
        public static void GetReferencesAndUsingDirectives(
            string version,
            string outputCommonPath,
            string outputBlizzardPath)
        {
            var outputCommon = !string.IsNullOrEmpty(outputCommonPath);
            var outputBlizzard = !string.IsNullOrEmpty(outputBlizzardPath);

            if (outputCommon || outputBlizzard)
            {
                var metadataReferences = GetBasicReferences().ToArray();

                const string CommonClassName = "Common";
                const string BlizzardClassName = "Blizzard";

                string apiNamespaceName = $@"War3Api.Generated.v{version.Replace('.', '_').Replace('-', '_')}";

                var cSharpDirective = GetCSharpDirective();

                if (!JassTranspiler.CompileCSharpFromJass(
                    Path.Combine("API", version, "common.j"),
                    null,
                    apiNamespaceName,
                    CommonClassName,
                    metadataReferences,
                    new[] { cSharpDirective },
                    out var commonReference,
                    out var commonDirective,
                    out var commonEmitResult,
                    OutputKind.DynamicallyLinkedLibrary,
                    true,
                    outputCommon ? outputCommonPath : null,
                    null,
                    null))
                {
                    foreach (var diagnostic in commonEmitResult.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }

                    throw new Exception();
                }

                if (outputBlizzard)
                {
                    metadataReferences = metadataReferences.Append(commonReference).ToArray();

                    if (!JassTranspiler.CompileCSharpFromJass(
                        Path.Combine("API", version, "Blizzard.j"),
                        null,
                        apiNamespaceName,
                        BlizzardClassName,
                        metadataReferences,
                        new[] { cSharpDirective, commonDirective },
                        out _,
                        out _,
                        out var blizzardEmitResult,
                        OutputKind.DynamicallyLinkedLibrary,
                        true,
                        outputBlizzardPath,
                        null,
                        null))
                    {
                        foreach (var diagnostic in blizzardEmitResult.Diagnostics)
                        {
                            Console.WriteLine(diagnostic);
                        }

                        throw new Exception();
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static IEnumerable<MetadataReference> GetBasicReferences()
        {
            yield return MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            yield return MetadataReference.CreateFromFile(Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51").Location);
#if NETCOREAPP3_0 // not tested, but this assembly was not required when this method was defined in a .net standard project
            yield return MetadataReference.CreateFromFile(Assembly.Load("System.Runtime, Version=4.2.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location);
#endif
            yield return MetadataReference.CreateFromFile(typeof(NativeLuaMemberAttribute).Assembly.Location);
        }

        private static UsingDirectiveSyntax GetCSharpDirective()
        {
            return SyntaxFactory.UsingDirective(
                SyntaxFactory.ParseName("War3Net.CodeAnalysis.Common")) // TODO: not hardcoded name
                .WithLeadingTrivia(SyntaxFactory.Trivia(
                    SyntaxFactory.PragmaWarningDirectiveTrivia(
                        SyntaxFactory.Token(SyntaxKind.DisableKeyword),
                        default(SeparatedSyntaxList<ExpressionSyntax>).AddRange(new[]
                        {
                            SyntaxFactory.ParseExpression("IDE0052"),
                            SyntaxFactory.ParseExpression("IDE1006"),
                            SyntaxFactory.ParseExpression("CS0626"),
                        }),
                        true)));
        }
    }
}