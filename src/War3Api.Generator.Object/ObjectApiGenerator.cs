// ------------------------------------------------------------------------------
// <copyright file="ObjectApiGenerator.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Humanizer;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Models;
using War3Net.Build.Object;
using War3Net.CodeAnalysis.CSharp;
using War3Net.Common.Extensions;
using War3Net.IO.Slk;

namespace War3Api.Generator.Object
{
    internal static class ObjectApiGenerator
    {
        internal const string NamespaceName = "War3Api.Object";

        private static IDictionary<string, string> _worldEditStrings;

        private static IList<EnumModel> _enumModels;
        private static IList<TypeModel> _typeModels;
        private static IDictionary<ObjectDataType, DataTypeModel> _dataTypeModels;

        private static string _inputFolder;
        private static string _outputFolder;
        private static bool _initialized = false;

        public static bool IsInitialized => _initialized;

        internal static void InitializeGenerator(string inputFolder, string outputFolder)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Already initialized.");
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            _inputFolder = inputFolder;
            _outputFolder = outputFolder;

            _worldEditStrings = GenerateWorldEditStringLookup();
            _enumModels = GenerateEnums().ToList();
            _typeModels = ModelService.GetTypeModels().ToList();
            _dataTypeModels = ModelService.GetDataTypeModels().ToDictionary(type => type.Type);

            GenerateDataConverter();

            foreach (var enumModel in _enumModels)
            {
                GenerateEnumFile(enumModel);
            }

            _initialized = true;
        }

        internal static void GenerateMember(BaseTypeDeclarationSyntax member, string namespaceName = null)
        {
            var outPath = _outputFolder;
            var fullNamespaceName = NamespaceName;
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                outPath = Path.Combine(_outputFolder, namespaceName);
                fullNamespaceName = $"{NamespaceName}.{namespaceName}";

                var dir = new DirectoryInfo(outPath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }

            var @namespace = SyntaxFactory.NamespaceDeclaration(
                SyntaxFactory.ParseName(fullNamespaceName),
                default,
                default,
                new SyntaxList<MemberDeclarationSyntax>(member));

            var compilationUnit = SyntaxFactory.CompilationUnit(
                default,
                SyntaxFactory.List(new[]
                {
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Net.Build.Common")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Net.Build.Object")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Net.Common.Extensions")),
                }),
                default,
                new SyntaxList<MemberDeclarationSyntax>(@namespace));

            using var fileStream = File.Create(Path.Combine(outPath, $"{member.Identifier.ValueText}.cs"));
            CompilationHelper.SerializeTo(compilationUnit, fileStream);
        }

        internal static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(SyntaxKind accessModifier, string className, IEnumerable<string> initializerValues, IEnumerable<(string field, ExpressionSyntax expression)> assignments)
        {
            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, Array.Empty<(string, string)>(), assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId") }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode") }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { ("ObjectDatabase", "db") }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId"), ("ObjectDatabase", "db") }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode"), ("ObjectDatabase", "db") }, assignments);
        }

        internal static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(string className, string objectTypeName, string objectTypeIdentifier)
        {
            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier) });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId") });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode") });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), ("ObjectDatabase", "db") });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId"), ("ObjectDatabase", "db") });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode"), ("ObjectDatabase", "db") });
        }

        internal static IEnumerable<MemberDeclarationSyntax> GetProperties(string className, IEnumerable<PropertyModel> properties, bool isAbstractClass = false)
        {
            return GetProperties(className, properties, isAbstractClass, className == "Doodad", isAbstractClass ? SyntaxKind.InternalKeyword : SyntaxKind.PrivateKeyword, null);
        }

        internal static IEnumerable<MemberDeclarationSyntax> GetProperties(string className, IEnumerable<PropertyModel> properties, bool isAbstractClass, bool usesVariation, SyntaxKind ctorAccessModifier, int? typeId)
        {
            var levelString = usesVariation ? "variation" : "level";

            var typeDict = _typeModels.ToDictionary(type => type.Name);

            var privateConstructorAssignments = new List<(string field, ExpressionSyntax expression)>();

            foreach (var propertyModel in properties)
            {
                var type = propertyModel.Type;
                if (!typeDict.TryGetValue(type, out var typeModel))
                {
                    throw new InvalidDataException($"Unknown type '{type}' for {className} property '{propertyModel.Rawcode}'.");
                }

                static string ParseMinMaxValue(object value)
                {
                    if (value is null)
                    {
                        return ModelService.GetKeywordText(SyntaxKind.NullKeyword);
                    }

                    if (value is int)
                    {
                        return value.ToString();
                    }

                    if (value is float)
                    {
                        return $"{value}f";
                    }

                    if (value is string @string)
                    {
                        if (int.TryParse(@string, out var @int))
                        {
                            return @int.ToString();
                        }

                        if (float.TryParse(@string, out var @float))
                        {
                            return $"{@float}f";
                        }

                        // todo: lookup
                        return 0.ToString();
                    }

                    throw new ArgumentException();
                }

                static string GetPrivateFieldName(string name)
                {
                    if (name.Contains('('))
                    {
                        throw new Exception();
                    }

                    return new string(name.Select((@char, i) => i == 0 ? @char.ToString().ToLower()[0] : @char).Prepend('_').ToArray());
                }

                var id = propertyModel.Rawcode.FromRawcode();
                // var valueName = propertyModel.DisplayName.Split('(')[0].Dehumanize();
                var valueName = new string(propertyModel.DisplayName.Where(@char => @char != '(' && @char != ')').ToArray()).Dehumanize();

                var propertyValueName = valueName;
                if (valueName == className)
                {
                    propertyValueName = new string(valueName.Append('_').ToArray());
                }

                var underlyingType = typeModel.Type;
                var dataTypeModel = _dataTypeModels[underlyingType];

                var optionalIsUnrealParameter = underlyingType == ObjectDataType.Real || underlyingType == ObjectDataType.Unreal
                    ? $", {(underlyingType == ObjectDataType.Unreal).ToString().ToLower()}"
                    : string.Empty;

                var fieldName = GetPrivateFieldName(nameof(ObjectModification));
                var simpleIdentifier = typeModel.IsBasicType ? propertyValueName : $"{valueName}Raw";

                if (propertyModel.Repeat)
                {
                    var propertyType = $"ObjectProperty<{dataTypeModel.Identifier}>";

                    var fieldIdentifier = GetPrivateFieldName(valueName);
                    var simpleFieldIdentifier = typeModel.IsBasicType ? fieldIdentifier : $"{fieldIdentifier}Raw";

                    var getterFuncName = $"Get{valueName}";
                    var setterFuncName = $"Set{valueName}";
                    var simpleGetterFuncName = typeModel.IsBasicType ? getterFuncName : $"{getterFuncName}Raw";
                    var simpleSetterFuncName = typeModel.IsBasicType ? setterFuncName : $"{setterFuncName}Raw";

                    privateConstructorAssignments.Add((simpleFieldIdentifier, SyntaxFactory.ParseExpression($"new Lazy<{propertyType}>(() => new {propertyType}({simpleGetterFuncName}, {simpleSetterFuncName}))")));

                    yield return SyntaxFactoryService.Field(
                        $"Lazy<{propertyType}>",
                        simpleFieldIdentifier,
                        true);

                    yield return SyntaxFactoryService.Property(
                        propertyType,
                        simpleIdentifier,
                        SyntaxFactory.ParseExpression($"{simpleFieldIdentifier}.Value"));

                    yield return SyntaxFactoryService.Method(
                        dataTypeModel.Identifier,
                        simpleGetterFuncName,
                        new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString) },
                        new[] { SyntaxFactory.ParseStatement($"return {fieldName}[{id}, {levelString}].{dataTypeModel.PropertyName};") });

                    yield return SyntaxFactoryService.Method(
                        SyntaxFactory.Token(SyntaxKind.VoidKeyword).ValueText,
                        simpleSetterFuncName,
                        new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString), (dataTypeModel.Identifier, "value") },
                        new[] { SyntaxFactory.ParseStatement($"{fieldName}[{id}, {levelString}] = new {nameof(ObjectDataModification)}({id}, {levelString}, value{optionalIsUnrealParameter});") });

                    if (!typeModel.IsBasicType)
                    {
                        var propertyTypeName = $"ObjectProperty<{typeModel.Identifier}>";

                        privateConstructorAssignments.Add((fieldIdentifier, SyntaxFactory.ParseExpression($"new Lazy<{propertyTypeName}>(() => new {propertyTypeName}({getterFuncName}, {setterFuncName}))")));

                        yield return SyntaxFactoryService.Field(
                            $"Lazy<{propertyTypeName}>",
                            fieldIdentifier,
                            true);

                        yield return SyntaxFactoryService.Property(
                            propertyTypeName,
                            propertyValueName,
                            SyntaxFactory.ParseExpression($"{fieldIdentifier}.Value"));

                        yield return SyntaxFactoryService.Method(
                            typeModel.Identifier,
                            getterFuncName,
                            new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString) },
                            new[] { SyntaxFactory.ParseStatement($"return {simpleGetterFuncName}({levelString}).To{typeModel.Identifier.Dehumanize()}(this);") });

                        yield return SyntaxFactoryService.Method(
                            SyntaxFactory.Token(SyntaxKind.VoidKeyword).ValueText,
                            setterFuncName,
                            new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString), (typeModel.Identifier, "value") },
                            new[] { SyntaxFactory.ParseStatement($"{simpleSetterFuncName}({levelString}, value.ToRaw({ParseMinMaxValue(propertyModel.MinVal)}, {ParseMinMaxValue(propertyModel.MaxVal)}));") });
                    }
                }
                else
                {
                    yield return SyntaxFactoryService.Property(
                        dataTypeModel.Identifier,
                        simpleIdentifier,
                        SyntaxFactoryService.Getter(SyntaxFactory.ParseExpression($"{fieldName}[{id}].{dataTypeModel.PropertyName}")),
                        SyntaxFactoryService.Setter(SyntaxFactory.ParseExpression($"{fieldName}[{id}] = new {nameof(ObjectDataModification)}({id}, value{optionalIsUnrealParameter})")));

                    if (!typeModel.IsBasicType)
                    {
                        var propertyTypeName = typeModel.Identifier;

                        yield return SyntaxFactoryService.Property(
                            propertyTypeName,
                            propertyValueName,
                            SyntaxFactoryService.Getter(SyntaxFactory.ParseExpression($"{simpleIdentifier}.To{propertyTypeName.Dehumanize()}(this)")),
                            SyntaxFactoryService.Setter(SyntaxFactory.ParseExpression($"{simpleIdentifier} = value.ToRaw({ParseMinMaxValue(propertyModel.MinVal)}, {ParseMinMaxValue(propertyModel.MaxVal)})")));
                    }
                }

#if false
                if (IsUnitClassAbstract)
                {
                    var accessor = SyntaxFactory.AccessorDeclaration(
                        SyntaxKind.GetAccessorDeclaration,
                        default,
                        default,
                        SyntaxFactory.Token(SyntaxKind.GetKeyword),
                        null,
                        null,
                        SyntaxFactory.Token(SyntaxKind.SemicolonToken));
                    var accessorDeclarationList = default(SyntaxList<AccessorDeclarationSyntax>);
                    var accessorList = SyntaxFactory.AccessorList(accessorDeclarationList.Add(accessor));

                    classMembers.Add(SyntaxFactory.PropertyDeclaration(
                        default,
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                            SyntaxFactory.Token(SyntaxKind.AbstractKeyword)),
                        SyntaxFactory.ParseTypeName(propertyTypeName),
                        null,
                        SyntaxFactory.Identifier($"Default{valueName}"),
                        accessorList,
                        null,
                        null));
                }
#endif
            }

            if (!typeId.HasValue)
            {
                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId") },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId") },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode") },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), ("ObjectDatabase", "db") },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId"), ("ObjectDatabase", "db") },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode"), ("ObjectDatabase", "db") },
                    privateConstructorAssignments);
            }
            else
            {
                foreach (var ctor in GetConstructors(ctorAccessModifier, className, new[] { typeId.Value.ToString() }, privateConstructorAssignments))
                {
                    yield return ctor;
                }
            }
        }

        internal static string Localize(string value)
        {
            // TODO: support string formatting with %
            // example: 'WESTRING_GEVAL_GBA3=Effect 3 - %s'
            while (true)
            {
                if (_worldEditStrings.TryGetValue(value, out var newValue))
                {
                    value = newValue;
                }
                else
                {
                    break;
                }
            }

            return value;
        }

        internal static SylkTable Localize(SylkTable table)
        {
            for (var row = 0; row < table.Rows; row++)
            {
                for (var column = 0; column < table.Columns; column++)
                {
                    if (table[column, row] is string @string)
                    {
                        if (@string != null)
                        {
                            table[column, row] = Localize(@string);
                        }
                    }
                }
            }

            return table;
        }

        private static IDictionary<string, string> GenerateWorldEditStringLookup()
        {
            var worldEditStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var stringsFile in new[] { PathConstants.WorldEditGameStringsPath, PathConstants.WorldEditStringsPath })
            {
                using (var worldEditStringsFile = File.OpenRead(Path.Combine(_inputFolder, stringsFile)))
                {
                    using (var worldEditStringsReader = new StreamReader(worldEditStringsFile))
                    {
                        while (!worldEditStringsReader.EndOfStream)
                        {
                            var line = worldEditStringsReader.ReadLine();
                            if (line.StartsWith("WESTRING", StringComparison.OrdinalIgnoreCase))
                            {
                                var splitPosition = line.IndexOf('=', StringComparison.Ordinal);
                                var key = line.Substring(0, splitPosition);
                                var value = line.Substring(splitPosition + 1);
                                if (!worldEditStrings.TryAdd(key, value))
                                {
                                    if (worldEditStrings[key] == value)
                                    {
                                        continue;
                                    }

                                    throw new Exception($"Key {key} already added:\r\nWas '{worldEditStrings[key]}'\r\nTried to add '{value}'");
                                }
                            }
                        }
                    }
                }
            }

            return worldEditStrings;
        }

        internal static void GenerateEnumFile(EnumModel enumModel)
        {
            var enumName = enumModel.Name.Dehumanize();
            var enumMembers = enumModel.Members;

            var duplicateNames = enumMembers.GroupBy(member => member.Name).Where(grouping => grouping.Count() > 1).Select(grouping => grouping.Key).ToHashSet();

            const string ListKeyword = "List";
            if (enumName.Contains(ListKeyword, StringComparison.Ordinal))
            {
                enumName = enumName.Remove(enumName.IndexOf(ListKeyword, StringComparison.Ordinal), ListKeyword.Length);
            }

            static string GetSummary(string displayName, int value)
            {
                if (displayName.StartsWith("WESTRING", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidDataException($"Found unlocalized WE string: '{displayName}'");
                }

                return $"{displayName.Humanize()}{(value > (1 << 24) ? $" ('{value.ToRawcode()}')" : string.Empty)}.";
            }

            GenerateMember(SyntaxFactory.EnumDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                SyntaxFactory.Identifier(enumName),
                null,
                SyntaxFactory.SeparatedList(
                    enumMembers.Select(enumMember =>
                        SyntaxFactory.EnumMemberDeclaration(
                            default,
                            SyntaxFactory.Identifier(enumMember.UniqueName ?? (duplicateNames.Contains(enumMember.Name) ? $"{enumMember.Name}_{enumMember.Value.ToRawcode()}" : enumMember.Name)),
                            SyntaxFactory.EqualsValueClause(
                                SyntaxFactory.ParseExpression(enumMember.ValueString)))
                        .WithLeadingTrivia(
                            SyntaxFactory.Trivia(
                                SyntaxFactory.DocumentationCommentTrivia(
                                    SyntaxKind.SingleLineDocumentationCommentTrivia,
                                    SyntaxFactory.List(new XmlNodeSyntax[]
                                    {
                                        SyntaxFactory.XmlText(
                                            SyntaxFactory.XmlTextLiteral(
                                                SyntaxFactory.TriviaList(
                                                    SyntaxFactory.DocumentationCommentExterior("///")),
                                                " ",
                                                " ",
                                                default)),
                                        SyntaxFactory.XmlSummaryElement(
                                            SyntaxFactory.XmlText(GetSummary(enumMember.DisplayName, enumMember.Value))),
                                        SyntaxFactory.XmlText(
                                            SyntaxFactory.XmlTextNewLine("\r\n", false)),
                                    }),
                                    SyntaxFactory.Token(SyntaxKind.EndOfDocumentationCommentToken))))))));
        }

        private static IEnumerable<EnumModel> GenerateEnums()
        {
            using (var unitEditorDataFile = File.OpenRead(Path.Combine(_inputFolder, PathConstants.EnumDataFilePath)))
            {
                using (var unitEditorDataReader = new StreamReader(unitEditorDataFile))
                {
                    var currentEnumType = string.Empty;
                    EnumModel currentEnumModel = null;
                    while (!unitEditorDataReader.EndOfStream)
                    {
                        var line = unitEditorDataReader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//", StringComparison.Ordinal))
                        {
                            continue;
                        }

                        if (line.StartsWith('[') && line.EndsWith(']'))
                        {
                            if (currentEnumModel != null)
                            {
                                yield return currentEnumModel;
                            }

                            currentEnumModel = new EnumModel(line[1..^1]);
                        }
                        else
                        {
                            var split = line.Split('=');
                            if (split.Length != 2)
                            {
                                throw new InvalidDataException(line);
                            }

                            var keyString = split[0];
                            var valueString = split[1];
                            if (string.Equals(keyString, "Sort", StringComparison.Ordinal))
                            {
                                continue;
                            }
                            else if (string.Equals(keyString, "NumValues", StringComparison.Ordinal))
                            {
                                if (int.Parse(valueString) != currentEnumModel.Members.Count)
                                {
                                    throw new InvalidDataException();
                                }
                            }
                            else if (keyString.EndsWith("_Alt") && int.TryParse(keyString.Split('_')[0], out var altKey))
                            {
                                if (!currentEnumModel.Members.Any(member => member.Value == altKey))
                                {
                                    throw new InvalidDataException(line);
                                }

                                currentEnumModel.Members.Single(member => member.Value == altKey).AlternativeName = valueString;
                            }
                            else if (int.TryParse(keyString, out var key))
                            {
                                var values = valueString.Split(',');
                                if (values.Length != 2 && values.Length != 3)
                                {
                                    throw new InvalidDataException(line);
                                }

                                var displayName = Localize(values[1]);
                                if (displayName.StartsWith('"') && displayName.EndsWith('"'))
                                {
                                    displayName = displayName[1..^1];
                                }

                                displayName = new string(displayName.Where(@char => @char != '&').ToArray());
                                var splitName = displayName.Split(',');
                                if (splitName.Length == 2 && string.Equals(splitName[0], splitName[1].Trim(), StringComparison.Ordinal))
                                {
                                    displayName = splitName[0];
                                }

                                var memberModel = new EnumMemberModel();
                                memberModel.DisplayName = displayName;
                                memberModel.GameVersion = values.Length == 3 ? int.Parse(values[2]) : 0;
                                if (int.TryParse(values[0], out var i))
                                {
                                    memberModel.Name = displayName.Dehumanize();
                                    memberModel.Value = i;
                                }
                                else
                                {
                                    memberModel.Value = key;
                                    if (string.IsNullOrWhiteSpace(values[0]))
                                    {
                                        memberModel.Name = displayName.Dehumanize();
                                    }
                                    else if (!string.IsNullOrWhiteSpace(values[0].Dehumanize()))
                                    {
                                        memberModel.Name = values[0].Dehumanize();
                                    }
                                    else
                                    {
                                        memberModel.Name = values[0];
                                    }
                                }

                                currentEnumModel.Members.Add(memberModel);
                            }
                            else
                            {
                                throw new Exception(line);
                            }
                        }
                    }

                    yield return currentEnumModel;
                }
            }
        }

        private static void GenerateDataConverter()
        {
            GenerateMember(SyntaxFactory.ClassDeclaration(
                default,
                new SyntaxTokenList(
                    SyntaxFactory.Token(SyntaxKind.InternalKeyword),
                    SyntaxFactory.Token(SyntaxKind.StaticKeyword)),
                SyntaxFactory.Identifier("DataConverterNew"),
                null,
                null,
                default,
                new SyntaxList<MemberDeclarationSyntax>(
                    _typeModels
                    .Where(typeModel => !typeModel.IsBasicType)
                    .Where(typeModel => !string.Equals(typeModel.Identifier, "string", StringComparison.OrdinalIgnoreCase))
                    .GroupBy(typeModel => typeModel.Identifier)
                    .Select(grouping => grouping.First())
                    .SelectMany(GetConvertMethods))));
        }

        private static IEnumerable<MethodDeclarationSyntax> GetConvertMethods(TypeModel typeModel)
        {
            var underlyingType = typeModel.Type;
            var dataTypeModel = _dataTypeModels[underlyingType];

            if (typeModel.Identifier.StartsWith("IList", StringComparison.Ordinal))
            {
                var genericType = typeModel.Identifier[6..^1];

                yield return SyntaxFactoryService.ExtensionMethod(
                    typeModel.Identifier,
                    $"ToIList{genericType.Dehumanize()}",
                    new[] { (dataTypeModel.Identifier, "value"), ("BaseObject", "baseObject"), },
                    $"return string.IsNullOrEmpty(value) || string.Equals(value, \"_\", StringComparison.Ordinal) ? Array.Empty<{genericType}>() : value.Split(',').Select(x => x.To{genericType.Dehumanize()}(baseObject)).ToArray()");

                yield return SyntaxFactoryService.ExtensionMethod(
                    dataTypeModel.Identifier,
                    "ToRaw",
                    new[] { (typeModel.Identifier, "list"), ("int?", "minValue"), ("int?", "maxValue"), },
                    "return (!maxValue.HasValue || list.Count <= maxValue.Value) ? $\"{string.Join(',', list.Select(value => value.ToRaw(null, null)))}\" : throw new ArgumentOutOfRangeException(nameof(list))");

                yield break;
            }

            yield return SyntaxFactoryService.ExtensionMethod(
                typeModel.Identifier,
                $"To{typeModel.Identifier}",
                new[] { (dataTypeModel.Identifier, "value"), ("BaseObject", "baseObject"), },
                new[]
                {
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.ParseExpression(
                            underlyingType == ObjectDataType.Int || underlyingType == ObjectDataType.Char
                                ? $"return ({typeModel.Identifier})value"
                                : $"return baseObject.Db.Get{typeModel.Identifier}(value.{nameof(War3Net.Common.Extensions.StringExtensions.FromRawcode)}())")),
                });

            yield return SyntaxFactoryService.ExtensionMethod(
                dataTypeModel.Identifier,
                "ToRaw",
                new[] { (typeModel.Identifier, "value"), ($"{dataTypeModel.Identifier}?", "minValue"), ($"{dataTypeModel.Identifier}?", "maxValue"), },
                new[]
                {
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.ParseExpression(
                            underlyingType == ObjectDataType.Int || underlyingType == ObjectDataType.Char
                                ? $"return ({dataTypeModel.Identifier})value"
                                : $"return value.Key.{nameof(Int32Extensions.ToRawcode)}()")),
                });
        }

        internal static bool ParseBool(this object obj)
        {
            return obj is int @int ? @int != 0 : (bool)obj;
        }
    }
}