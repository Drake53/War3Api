// ------------------------------------------------------------------------------
// <copyright file="ObjectApiGenerator.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Humanizer;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
using War3Api.Generator.Object.Models;

using War3Net.Build.Object;
using War3Net.Common.Extensions;
using War3Net.IO.Slk;

namespace War3Api.Generator.Object
{
    internal static class ObjectApiGenerator
    {
        internal const string NamespaceName = "War3Api.Object";
        internal const string ListTypeName = nameof(IEnumerable);

        private static IDictionary<string, string> _worldEditStrings;
        private static IDictionary<string, IDictionary<string, string[]>> _worldEditData;

        private static IList<TypeModel> _typeModels;
        private static IDictionary<string, TypeModel> _typeModelsDict;
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
            _worldEditData = GenerateWorldEditDataLookup();
            _typeModels = ModelService.GetTypeModels().ToList();
            _typeModelsDict = _typeModels.ToDictionary(type => type.Name);
            _dataTypeModels = ModelService.GetDataTypeModels().ToDictionary(type => type.Type);

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
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Diagnostics.CodeAnalysis")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Api.Object.Abilities")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Api.Object.Enums")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Net.Build.Object")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("War3Net.Common.Extensions")),
                }),
                default,
                new SyntaxList<MemberDeclarationSyntax>(@namespace));

            using var fileStream = File.Create(Path.Combine(outPath, $"{member.Identifier.ValueText}.cs"));
            using var writer = new StreamWriter(fileStream);

            compilationUnit.NormalizeWhitespace().WriteTo(writer);
        }

        internal static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(SyntaxKind accessModifier, string className, IEnumerable<string> initializerValues, IEnumerable<(string field, ExpressionSyntax expression)> assignments)
        {
            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, Array.Empty<(string, string)>(), assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId") }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode") }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) }, assignments);

            yield return SyntaxFactoryService.Constructor(accessModifier, className, false, initializerValues, new[] { (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) }, assignments);
        }

        internal static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(string className, string objectTypeName)
        {
            var objectTypeIdentifier = objectTypeName.ToCamelCase(true);

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier) });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId") });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode") });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) });

            yield return SyntaxFactoryService.Constructor(SyntaxKind.PublicKeyword, className, new[] { (objectTypeName, objectTypeIdentifier), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) });
        }

        internal static string CreatePropertyIdentifierName(string field, string category, string displayName)
        {
            var sb = new StringBuilder();
            sb.Append(Localize(LookupCategory(category)).Replace("&", string.Empty, StringComparison.Ordinal));

            var localizedDisplayName = Localize(displayName);
            sb.Append(localizedDisplayName.Contains('%', StringComparison.Ordinal) ? field : localizedDisplayName);

            return sb.ToString().ToIdentifier();
        }

        internal static void EnsurePropertyNamesUnique(IEnumerable<PropertyModel> properties, bool useSpecific = false)
        {
            if (useSpecific)
            {
                var knownSpecifics = properties.SelectMany(propertyModel => propertyModel.Specifics).ToHashSet();

                foreach (var specific in knownSpecifics)
                {
                    var propertiesSubset = properties.Where(propertyModel => propertyModel.Specifics.IsEmpty || propertyModel.Specifics.Contains(specific)).ToList();
                    var specificDuplicateNames = propertiesSubset.FindDuplicates(propertyModel => propertyModel.IdentifierName, StringComparer.Ordinal);

                    foreach (var propertyModel in propertiesSubset)
                    {
                        if (!propertyModel.Specifics.IsEmpty && specificDuplicateNames.Contains(propertyModel.IdentifierName))
                        {
                            propertyModel.SpecificUniqueNames.Add(specific, $"{propertyModel.IdentifierName}_{propertyModel.Rawcode}");
                        }
                    }
                }

                foreach (var propertyModel in properties)
                {
                    if (!propertyModel.Specifics.IsEmpty)
                    {
                        propertyModel.UniqueName = propertyModel.IdentifierName;
                    }
                }

                EnsurePropertyNamesUnique(properties.Where(propertyModel => propertyModel.Specifics.IsEmpty));
            }
            else
            {
                var duplicateNames = properties.FindDuplicates(propertyModel => propertyModel.IdentifierName, StringComparer.Ordinal);

                foreach (var propertyModel in properties)
                {
                    propertyModel.UniqueName = duplicateNames.Contains(propertyModel.IdentifierName)
                        ? $"{propertyModel.IdentifierName}_{propertyModel.Rawcode}"
                        : propertyModel.IdentifierName;
                }
            }
        }

        internal static EnumMemberModel CreateEnumMemberModel(string name, string value)
        {
            var enumMemberModel = new EnumMemberModel();

            enumMemberModel.DisplayName = Localize(name);
            enumMemberModel.Name = enumMemberModel.DisplayName.Dehumanize();
            enumMemberModel.Value = value.FromRawcode();

            if (char.IsDigit(enumMemberModel.Name[0]))
            {
                enumMemberModel.Name = "_" + enumMemberModel.Name;
            }

            return enumMemberModel;
        }

        internal static IEnumerable<MemberDeclarationSyntax> GetProperties(string className, string objectTypeName, IEnumerable<PropertyModel> properties, bool isAbstractClass = false)
        {
            var mapToUsesVariationBool = new Dictionary<string, bool?>
            {
                { "Unit", null },
                { "Item", null },
                { "Destructable", null },
                { "Doodad", true },
                { "Ability", false },
                { "Buff", null },
                { "Upgrade", false },
            };

            return GetProperties(className, objectTypeName, properties, mapToUsesVariationBool[className], isAbstractClass, null);
        }

        internal static IEnumerable<MemberDeclarationSyntax> GetProperties(string className, string objectTypeName, IEnumerable<PropertyModel> properties, bool? usesVariation, bool isAbstractClass, int? typeId)
        {
            const string ModificationsPropertyName = "Modifications";

            var modificationsFieldName = ModificationsPropertyName.ToCamelCase(true, true);
            var objectModificationTypeName = usesVariation.HasValue ? usesVariation.Value ? nameof(VariationObjectModification) : nameof(LevelObjectModification) : nameof(SimpleObjectModification);
            var dataTypeName = usesVariation.HasValue ? usesVariation.Value ? nameof(VariationObjectDataModification) : nameof(LevelObjectDataModification) : nameof(SimpleObjectDataModification);

            var privateConstructorAssignments = new List<(string field, ExpressionSyntax expression)>();

            if (!typeId.HasValue)
            {
                var dictTypeName = usesVariation.HasValue ? usesVariation.Value ? DataConstants.VariationDictClassName : DataConstants.LevelDictClassName : DataConstants.SimpleDictClassName;

                yield return SyntaxFactoryService.Field(
                    dictTypeName,
                    modificationsFieldName,
                    null,
                    isAbstractClass ? SyntaxKind.ProtectedKeyword : SyntaxKind.PrivateKeyword);

                privateConstructorAssignments.Add((modificationsFieldName, SyntaxFactory.ParseExpression("new(this)")));

                yield return SyntaxFactoryService.Property(
                    dictTypeName,
                    ModificationsPropertyName,
                    SyntaxFactory.ParseExpression(modificationsFieldName));

                var classVariableName = className.ToCamelCase(true);
                yield return SyntaxFactory.ConversionOperatorDeclaration(
                    default,
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                        SyntaxFactory.Token(SyntaxKind.StaticKeyword)),
                    SyntaxFactory.Token(SyntaxKind.ExplicitKeyword),
                    SyntaxFactory.Token(SyntaxKind.OperatorKeyword),
                    SyntaxFactory.ParseTypeName(objectModificationTypeName),
                    SyntaxFactory.ParameterList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Parameter(
                        default,
                        default,
                        SyntaxFactory.ParseTypeName(className),
                        SyntaxFactory.Identifier(classVariableName),
                        null))),
                    null,
                    SyntaxFactory.ArrowExpressionClause(SyntaxFactory.ParseExpression(
                        $"new {objectModificationTypeName} {{ OldId = {classVariableName}.OldId, NewId = {classVariableName}.NewId, Modifications = {classVariableName}.{ModificationsPropertyName}.ToList() }}")),
                    SyntaxFactory.Token(SyntaxKind.SemicolonToken));

                yield return SyntaxFactoryService.Method(
                    new[] { SyntaxKind.PublicKeyword },
                    "void",
                    "AddModifications",
                    new[]
                    {
                        ($"List<{dataTypeName}>", "modifications"),
                    },
                    new[]
                    {
                        SyntaxFactory.ForEachStatement(
                            SyntaxFactory.ParseTypeName("var"),
                            "modification",
                            SyntaxFactory.ParseExpression("modifications"),
                            SyntaxFactory.ParseStatement($"{modificationsFieldName}[modification.Id] = modification;")),
                    });

                if (!usesVariation.HasValue)
                {
                    yield return SyntaxFactory.ParseMemberDeclaration(@"internal override bool TryGetSimpleModifications([NotNullWhen(true)] out SimpleObjectDataModifications? modifications)
{
    modifications = _modifications;
    return true;
}");
                }
                else if (usesVariation.Value)
                {
                    yield return SyntaxFactory.ParseMemberDeclaration(@"internal override bool TryGetVariationModifications([NotNullWhen(true)] out VariationObjectDataModifications? modifications)
{
    modifications = _modifications;
    return true;
}");
                }
                else
                {
                    yield return SyntaxFactory.ParseMemberDeclaration(@"internal override bool TryGetLevelModifications([NotNullWhen(true)] out LevelObjectDataModifications? modifications)
{
    modifications = _modifications;
    return true;
}");
                }
            }

            foreach (var propertyModel in properties)
            {
                var uniqueName = typeId.HasValue && propertyModel.SpecificUniqueNames.TryGetValue(typeId.Value, out var specificUniqueName)
                    ? specificUniqueName
                    : propertyModel.UniqueName;

                var type = propertyModel.Type;
                if (!_typeModelsDict.TryGetValue(type, out var typeModel))
                {
                    throw new InvalidDataException($"Unknown type '{type}' for {className} property {uniqueName} ('{propertyModel.Rawcode}').");
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
                        if (string.Equals(@string, "TTDesc", StringComparison.Ordinal) ||
                            string.Equals(@string, "TTName", StringComparison.Ordinal))
                        {
                            return ModelService.GetKeywordText(SyntaxKind.NullKeyword);
                        }

                        throw new KeyNotFoundException($"Unknown constant: {@string}");
                    }

                    throw new ArgumentException();
                }

                var id = propertyModel.Rawcode.FromRawcode();

                var propertyValueName = uniqueName;
                if (string.Equals(propertyValueName, className, StringComparison.Ordinal))
                {
                    propertyValueName += "_";
                }

                var identifier = typeModel.FullIdentifier;
                var dataTypeModel = _dataTypeModels[typeModel.Type];

                var hasRawProperty = typeModel.Category != TypeModelCategory.Basic || dataTypeModel.Type != dataTypeModel.UnderlyingType;

                var simpleIdentifier = hasRawProperty ? $"{uniqueName}Raw" : propertyValueName;

                if (propertyModel.Repeat > 0)
                {
                    var levelString = usesVariation.Value ? "variation" : "level";

                    var propertyType = $"ObjectProperty<{dataTypeModel.Identifier}>";

                    var fieldIdentifier = uniqueName.ToCamelCase(true, true);
                    var simpleFieldIdentifier = hasRawProperty ? $"{fieldIdentifier}Raw" : fieldIdentifier;

                    var getterFuncName = $"Get{uniqueName}";
                    var setterFuncName = $"Set{uniqueName}";
                    var simpleGetterFuncName = hasRawProperty ? $"{getterFuncName}Raw" : getterFuncName;
                    var simpleSetterFuncName = hasRawProperty ? $"{setterFuncName}Raw" : setterFuncName;

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
                        new[] { SyntaxFactory.ParseStatement($"return {modificationsFieldName}.GetModification({id}, {levelString}).{dataTypeModel.PropertyName};") });

                    yield return SyntaxFactoryService.Method(
                        SyntaxFactory.Token(SyntaxKind.VoidKeyword).ValueText,
                        simpleSetterFuncName,
                        new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString), (dataTypeModel.Identifier, "value") },
                        new[] { SyntaxFactory.ParseStatement($"{modificationsFieldName}[{id}, {levelString}] = new {dataTypeName} {{ Id = {id}, Type = ObjectDataType.{dataTypeModel.UnderlyingType}, Value = value, {(usesVariation.Value ? "Variation" : "Level")} = {levelString}{(propertyModel.Data > 0 ? $", Pointer = {propertyModel.Data}" : string.Empty)} }};") });

                    const string isModifiedPropertyType = "ReadOnlyObjectProperty<bool>";

                    var isModifiedFieldIdentifier = $"Is{uniqueName}Modified".ToCamelCase(true, true);
                    var isModifiedGetterFuncName = $"GetIs{uniqueName}Modified";

                    privateConstructorAssignments.Add((isModifiedFieldIdentifier, SyntaxFactory.ParseExpression($"new Lazy<{isModifiedPropertyType}>(() => new {isModifiedPropertyType}({isModifiedGetterFuncName}))")));

                    yield return SyntaxFactoryService.Field(
                        $"Lazy<{isModifiedPropertyType}>",
                        isModifiedFieldIdentifier,
                        true);

                    yield return SyntaxFactoryService.Property(
                        isModifiedPropertyType,
                        $"Is{uniqueName}Modified",
                        SyntaxFactory.ParseExpression($"{isModifiedFieldIdentifier}.Value"));

                    yield return SyntaxFactoryService.Method(
                        "bool",
                        isModifiedGetterFuncName,
                        new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString) },
                        new[] { SyntaxFactory.ParseStatement($"return {modificationsFieldName}.ContainsKey({id}, {levelString});") });

                    if (hasRawProperty)
                    {
                        var propertyTypeName = $"ObjectProperty<{identifier}>";

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
                            identifier,
                            getterFuncName,
                            new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString) },
                            new[] { SyntaxFactory.ParseStatement($"return {simpleGetterFuncName}({levelString}).To{identifier.Dehumanize()}(this);") });

                        yield return SyntaxFactoryService.Method(
                            SyntaxFactory.Token(SyntaxKind.VoidKeyword).ValueText,
                            setterFuncName,
                            new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, levelString), (identifier, "value") },
                            new[] { SyntaxFactory.ParseStatement($"{simpleSetterFuncName}({levelString}, value.ToRaw({ParseMinMaxValue(propertyModel.MinVal)}, {ParseMinMaxValue(propertyModel.MaxVal)}));") });
                    }
                }
                else
                {
                    yield return SyntaxFactoryService.Property(
                        dataTypeModel.Identifier,
                        simpleIdentifier,
                        SyntaxFactoryService.Getter(SyntaxFactory.ParseExpression($"{modificationsFieldName}.GetModification({id}).{dataTypeModel.PropertyName}")),
                        SyntaxFactoryService.Setter(SyntaxFactory.ParseExpression($"{modificationsFieldName}[{id}] = new {dataTypeName} {{ Id = {id}, Type = ObjectDataType.{dataTypeModel.UnderlyingType}, Value = value{(usesVariation.HasValue ? $", {(usesVariation.Value ? "Variation" : "Level")} = 0" : string.Empty)}{(propertyModel.Data > 0 ? $", Pointer = {propertyModel.Data}" : string.Empty)} }}")));

                    yield return SyntaxFactoryService.Property(
                        "bool",
                        $"Is{uniqueName}Modified",
                        SyntaxFactory.ParseExpression($"{modificationsFieldName}.ContainsKey({id})"));

                    if (hasRawProperty)
                    {
                        var propertyTypeName = identifier;

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
                var ctorAccessModifier = isAbstractClass ? SyntaxKind.InternalKeyword : SyntaxKind.PrivateKeyword;

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
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "newId"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) },
                    privateConstructorAssignments);

                yield return SyntaxFactoryService.Constructor(
                    ctorAccessModifier,
                    className,
                    new[] { (SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText, "oldId"), (SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText, "newRawcode"), (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName) },
                    privateConstructorAssignments);
            }
            else
            {
                foreach (var ctor in GetConstructors(SyntaxKind.PublicKeyword, className, new[] { typeId.Value.ToString() }, privateConstructorAssignments))
                {
                    yield return ctor;
                }
            }
        }

        internal static IEnumerable<MemberDeclarationSyntax> GetLoaderMethods(
            IEnumerable<EnumMemberModel> members,
            IEnumerable<PropertyModel> properties,
            IDictionary<string, TableModel> dataTables,
            string objectClassName,
            string objectTypeName,
            bool isAbstractClass)
        {
            var objectTypeVariableName = objectTypeName.ToCamelCase(true);

            var switchExpressionArms = members
                .Select(objectType => SyntaxFactory.SwitchExpressionArm(
                    SyntaxFactory.ConstantPattern(SyntaxFactory.ParseExpression($"{objectTypeName}.{objectType.UniqueName}")),
                    SyntaxFactory.ParseExpression($"Load{objectType.UniqueName}({DataConstants.DatabaseVariableName})")))
                .ToList();

            switchExpressionArms.Add(SyntaxFactory.SwitchExpressionArm(
                SyntaxFactory.DiscardPattern(),
                SyntaxFactory.ParseExpression($"throw new System.ComponentModel.InvalidEnumArgumentException(nameof({objectTypeVariableName}), (int){objectTypeVariableName}, typeof({objectTypeName}))")));

            yield return SyntaxFactoryService.Method(
                new[] { SyntaxKind.PublicKeyword },
                objectClassName,
                "Load",
                new[]
                {
                    (objectTypeName, objectTypeVariableName),
                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                },
                new[]
                {
                    SyntaxFactory.ReturnStatement(SyntaxFactory.SwitchExpression(
                        SyntaxFactory.ParseExpression(objectTypeVariableName),
                        SyntaxFactory.SeparatedList(switchExpressionArms))),
                });

            foreach (var member in members)
            {
                yield return LoaderLoadMethod(member, properties, dataTables, objectClassName, objectTypeName, isAbstractClass);
            }
        }

        private static MethodDeclarationSyntax LoaderLoadMethod(
            EnumMemberModel objectType,
            IEnumerable<PropertyModel> properties,
            IDictionary<string, TableModel> dataTables,
            string objectClassName,
            string objectTypeName,
            bool isAbstractClass)
        {
            var objectVariableName = objectClassName.ToCamelCase(true);

            var statements = new List<StatementSyntax>();

            var objectCreationArgumentList = new List<ArgumentSyntax>();
            if (!isAbstractClass)
            {
                objectCreationArgumentList.Add(SyntaxFactory.Argument(SyntaxFactory.ParseExpression($"{objectTypeName}.{objectType.UniqueName}")));
            }

            objectCreationArgumentList.Add(SyntaxFactory.Argument(SyntaxFactory.ParseExpression(DataConstants.DatabaseVariableName)));

            statements.Add(SyntaxFactory.LocalDeclarationStatement(SyntaxFactory.VariableDeclaration(
                SyntaxFactory.ParseTypeName("var"),
                SyntaxFactory.SingletonSeparatedList(SyntaxFactory.VariableDeclarator(
                    SyntaxFactory.Identifier(objectVariableName),
                    null,
                    SyntaxFactory.EqualsValueClause(SyntaxFactory.ObjectCreationExpression(
                        SyntaxFactory.ParseTypeName(isAbstractClass ? objectType.UniqueName : objectClassName),
                        SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(objectCreationArgumentList)),
                        null)))))));

            var dataRows = new Dictionary<string, int>(StringComparer.Ordinal);

            var objectProperties = properties
                .Where(propertyModel => propertyModel.Specifics.IsEmpty || propertyModel.Specifics.Contains(objectType.Value))
                .ToList();

            var format = $"D{(objectProperties.Select(propertyModel => propertyModel.Repeat).Max() >= 10 ? "2" : "1")}";

            foreach (var propertyModel in objectProperties)
            {
                if (!dataTables.TryGetValue(propertyModel.DataSource, out var dataTable))
                {
                    continue;
                }

                if (!dataRows.TryGetValue(propertyModel.DataSource, out var dataRow))
                {
                    for (var row = 1; row <= dataTable.Table.Rows; row++)
                    {
                        if (objectType.Value == ((string)dataTable.Table[dataTable.TableKeyColumn, row]).FromRawcode())
                        {
                            dataRows.Add(propertyModel.DataSource, row);
                            dataRow = row;
                            break;
                        }
                    }
                }

                if (dataRow == default)
                {
                    continue;
                }

                var uniqueName = propertyModel.SpecificUniqueNames.TryGetValue(objectType.Value, out var specificUniqueName)
                    ? specificUniqueName
                    : propertyModel.UniqueName;

                var type = propertyModel.Type;
                if (!_typeModelsDict.TryGetValue(type, out var typeModel))
                {
                    throw new InvalidDataException($"Unknown type '{type}' for property {uniqueName} ('{propertyModel.Rawcode}').");
                }

                var dataTypeModel = _dataTypeModels[typeModel.Type];

                var hasRawProperty = typeModel.Category != TypeModelCategory.Basic || dataTypeModel.Type != dataTypeModel.UnderlyingType;
                var isStringProperty = typeModel.Category != TypeModelCategory.Basic && typeModel.Category != TypeModelCategory.EnumInt && typeModel.Category != TypeModelCategory.EnumFlags;

                var left = hasRawProperty ? $"{objectVariableName}.{uniqueName}Raw" : $"{objectVariableName}.{uniqueName}";

                if (propertyModel.Repeat > 0)
                {
                    for (var i = 1; i <= propertyModel.Repeat; i++)
                    {
                        var columnName = propertyModel.Data > 0
                            ? $"{propertyModel.Name}{(char)(propertyModel.Data + 'A' - 1)}{i.ToString(format)}"
                            : $"{propertyModel.Name}{i.ToString(format)}";

                        var column = dataTable.Table[columnName].Cast<int?>().SingleOrDefault();
                        if (column.HasValue)
                        {
                            var value = dataTable.Table[column.Value, dataRow];
                            var propertyValue = GetPropertyValue(isStringProperty ? ObjectDataType.String : dataTypeModel.UnderlyingType, value);

                            statements.Add(SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.ElementAccessExpression(
                                    SyntaxFactory.ParseExpression(left),
                                    SyntaxFactory.BracketedArgumentList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Argument(SyntaxFactory.ParseExpression(i.ToString()))))),
                                SyntaxFactory.ParseExpression(propertyValue))));
                        }
                    }
                }
                else
                {
                    var columnName = propertyModel.Name;

                    var column = dataTable.Table[columnName].Cast<int?>().SingleOrDefault();
                    if (column.HasValue)
                    {
                        var value = dataTable.Table[column.Value, dataRow];
                        var propertyValue = GetPropertyValue(isStringProperty ? ObjectDataType.String : dataTypeModel.UnderlyingType, value);

                        statements.Add(SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.ParseExpression(left),
                            SyntaxFactory.ParseExpression(propertyValue))));
                    }
                }
            }

            statements.Add(SyntaxFactory.ReturnStatement(SyntaxFactory.ParseExpression(objectVariableName)));

            return SyntaxFactoryService.Method(
                new[] { SyntaxKind.ProtectedKeyword, SyntaxKind.VirtualKeyword },
                isAbstractClass ? objectType.UniqueName : objectClassName,
                $"Load{objectType.UniqueName}",
                new[]
                {
                    (DataConstants.DatabaseClassName, DataConstants.DatabaseVariableName),
                },
                statements);
        }

        private static string GetPropertyValue(ObjectDataType objectDataType, object value)
        {
            return objectDataType switch
            {
                ObjectDataType.Int => value is null || value is string ? $"{default(int)}" : $"{value}",
                ObjectDataType.Real => value is null || value is string ? $"{default(float)}f" : $"{value}f",
                ObjectDataType.Unreal => value is null || value is string ? $"{default(float)}f" : $"{value}f",
                ObjectDataType.String => value is null ? "null" : $"\"{((string)value).Replace("\\", @"\\")}\"",

                _ => throw new InvalidEnumArgumentException(nameof(objectDataType), (int)objectDataType, typeof(ObjectDataType)),
            };
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
                                    /*if (string.Equals(worldEditStrings[key], value, StringComparison.Ordinal))
                                    {
                                        continue;
                                    }

                                    throw new Exception($"Key {key} already added:\r\nWas '{worldEditStrings[key]}'\r\nTried to add '{value}'");*/
                                }
                            }
                        }
                    }
                }
            }

            return worldEditStrings;
        }

        internal static string LookupCategory(string category)
        {
            return _worldEditData["ObjectEditorCategories"][category].Single();
        }

        private static IDictionary<string, IDictionary<string, string[]>> GenerateWorldEditDataLookup()
        {
            return GetWorldEditData().ToDictionary(pair => pair.Item1, pair => pair.Item2);
        }

        private static IEnumerable<(string, IDictionary<string, string[]>)> GetWorldEditData()
        {
            var worldEditData = new Dictionary<string, Dictionary<string, string[]>>(StringComparer.Ordinal);
            using (var worldEditDataFile = File.OpenRead(Path.Combine(_inputFolder, PathConstants.WorldEditDataPath)))
            {
                using (var worldEditDataFileReader = new StreamReader(worldEditDataFile))
                {
                    string currentDataSetKey = null;
                    Dictionary<string, string[]> currentDataSet = null;
                    while (!worldEditDataFileReader.EndOfStream)
                    {
                        var line = worldEditDataFileReader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//", StringComparison.Ordinal))
                        {
                            continue;
                        }

                        if (line.StartsWith('[') && line.EndsWith(']'))
                        {
                            if (currentDataSet != null)
                            {
                                yield return (currentDataSetKey, currentDataSet);
                            }

                            currentDataSetKey = line[1..^1];
                            currentDataSet = new Dictionary<string, string[]>();
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
                            if (keyString.StartsWith("Num", StringComparison.Ordinal))
                            {
                                if (currentDataSet.Count == 0)
                                {
                                    continue;
                                }

                                if (int.Parse(valueString) != currentDataSet.Count)
                                {
                                    throw new InvalidDataException();
                                }
                            }
                            else
                            {
                                currentDataSet.Add(keyString, valueString.Split(','));
                            }
                        }
                    }

                    yield return (currentDataSetKey, currentDataSet);
                }
            }
        }

        internal static void GenerateEnumFile(EnumModel enumModel)
        {
            var enumName = enumModel.Name;
            var enumMembers = enumModel.Members;

            // Can not use EndsWith("Flags"), because FullFlags is not really a Flags enum.
            var flagEnumNames = new HashSet<string>(StringComparer.Ordinal)
            {
                "ChannelFlags",
                "InteractionFlags",
                "MorphFlags",
                "PickFlags",
                "SilenceFlags",
                "StackFlags",
                "VersionFlags",
            };

            var isFlagsEnum = flagEnumNames.Contains(enumName);
            var isUnusedEnum = enumModel.Unused;

            static string GetSummary(string displayName, int value)
            {
                if (displayName.StartsWith("WESTRING", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidDataException($"Found unlocalized WE string: '{displayName}'");
                }

                return $"{displayName.Humanize()}{(value > (1 << 24) ? $" ('{value.ToRawcode()}')" : string.Empty)}.";
            }

            var attributesList = new List<AttributeSyntax>();
            if (isFlagsEnum)
            {
                attributesList.Add(SyntaxFactory.Attribute(SyntaxFactory.ParseName(nameof(FlagsAttribute))));
            }

            if (isUnusedEnum)
            {
                attributesList.Add(SyntaxFactory.Attribute(SyntaxFactory.ParseName(nameof(ObsoleteAttribute))));
            }

            var enumDeclaration = SyntaxFactory.EnumDeclaration(
                attributesList.Any()
                    ? SyntaxFactory.SingletonList(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(attributesList)))
                    : default,
                SyntaxTokenList.Create(SyntaxFactory.Token(isUnusedEnum ? SyntaxKind.InternalKeyword : SyntaxKind.PublicKeyword)),
                SyntaxFactory.Identifier(enumName),
                null,
                SyntaxFactory.SeparatedList(
                    enumMembers.Select(enumMember =>
                        SyntaxFactory.EnumMemberDeclaration(
                            default,
                            SyntaxFactory.Identifier(enumMember.UniqueName),
                            SyntaxFactory.EqualsValueClause(
                                SyntaxFactory.ParseExpression(isFlagsEnum ? $"1 << {enumMember.Value}" : enumMember.ValueString)))
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
                                    SyntaxFactory.Token(SyntaxKind.EndOfDocumentationCommentToken)))))));

            GenerateMember(enumDeclaration, "Enums");
        }

        private static IEnumerable<EnumModel> GetEnums()
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

                            var enumTypeModel = _typeModels.Single(typeModel => string.Equals(typeModel.Name, line[1..^1], StringComparison.Ordinal));

                            currentEnumModel = new EnumModel(enumTypeModel.Identifier, enumTypeModel.Category == TypeModelCategory.EnumUnused);
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

                                // currentEnumModel.Members.Single(member => member.Value == altKey).AlternativeName = valueString;
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
                                // memberModel.GameVersion = values.Length == 3 ? int.Parse(values[2]) : 0;
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
                                throw new InvalidDataException(line);
                            }
                        }
                    }

                    yield return currentEnumModel;
                }
            }
        }

        internal static void GenerateDataConverter()
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
                    .Where(typeModel => typeModel.Category != TypeModelCategory.String && typeModel.Type != ObjectDataType.Unreal)
                    // Use GroupBy to prevent generating duplicate methods for abilList/heroAbilList and buffList/effectList.
                    .GroupBy(typeModel => typeModel.FullIdentifier)
                    .Select(grouping => grouping.First())
                    .SelectMany(GetConvertMethods))));
        }

        internal static void GenerateEnums()
        {
            var destructableCategory = new EnumModel("DestructableCategory");
            foreach (var member in _worldEditData["DestructibleCategories"])
            {
                var memberModel = new EnumMemberModel();

                var name = Localize(member.Value.First());
                memberModel.Name = name.Dehumanize();
                memberModel.DisplayName = name;
                memberModel.Value = char.Parse(member.Key);
                memberModel.IsValueChar = true;

                destructableCategory.Members.Add(memberModel);
            }

            destructableCategory.EnsureMemberNamesUnique();

            GenerateEnumFile(destructableCategory);

            var doodadCategory = new EnumModel("DoodadCategory");
            foreach (var member in _worldEditData["DoodadCategories"])
            {
                var memberModel = new EnumMemberModel();

                var name = Localize(member.Value.First());
                memberModel.Name = name.Dehumanize();
                memberModel.DisplayName = name;
                memberModel.Value = char.Parse(member.Key);
                memberModel.IsValueChar = true;

                doodadCategory.Members.Add(memberModel);
            }

            doodadCategory.EnsureMemberNamesUnique();

            GenerateEnumFile(doodadCategory);

            var enumModels = GetEnums()
                .GroupBy(enumModel => enumModel.Name, StringComparer.Ordinal)
                .Select(grouping => grouping.First())
                .ToList();

            foreach (var enumModel in enumModels)
            {
                enumModel.EnsureMemberNamesUnique();

                GenerateEnumFile(enumModel);
            }
        }

        private static IEnumerable<MethodDeclarationSyntax> GetConvertMethods(TypeModel typeModel)
        {
            var underlyingType = typeModel.Type;
            var dataTypeModel = _dataTypeModels[underlyingType];

            switch (typeModel.Category)
            {
                case TypeModelCategory.EnumUnused:
                case TypeModelCategory.Basic:
                    break;

                case TypeModelCategory.EnumString:
                case TypeModelCategory.EnumLowercase:
                    var keepCasing = typeModel.Category == TypeModelCategory.EnumString;
                    var ignoreCase = ModelService.GetKeywordText(keepCasing ? SyntaxKind.FalseKeyword : SyntaxKind.TrueKeyword);
                    var toLower = keepCasing ? string.Empty : $".{nameof(string.ToLowerInvariant)}()";

                    yield return SyntaxFactoryService.ExtensionMethod(
                        typeModel.Identifier,
                        $"To{typeModel.Identifier}",
                        new[] { (dataTypeModel.Identifier, "value"), ("BaseObject", "baseObject"), },
                        new[]
                        {
                            // todo: use TryParse instead? since that one is generic
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression($"return ({typeModel.Identifier})Enum.Parse(typeof({typeModel.Identifier}), value, {ignoreCase})")),
                        });

                    yield return SyntaxFactoryService.ExtensionMethod(
                        dataTypeModel.Identifier,
                        "ToRaw",
                        new[] { (typeModel.Identifier, "value"), ("int?", "minValue"), ("int?", "maxValue"), },
                        new[]
                        {
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression($"return value.ToString(){toLower}")),
                        });

                    break;

                case TypeModelCategory.EnumChar:
                    yield return SyntaxFactoryService.ExtensionMethod(
                        typeModel.Identifier,
                        $"To{typeModel.Identifier}",
                        new[] { (dataTypeModel.Identifier, "value"), ("BaseObject", "baseObject"), },
                        new[]
                        {
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression($"return ({typeModel.Identifier})char.Parse(value)")),
                        });

                    yield return SyntaxFactoryService.ExtensionMethod(
                        dataTypeModel.Identifier,
                        "ToRaw",
                        new[] { (typeModel.Identifier, "value"), ("int?", "minValue"), ("int?", "maxValue"), },
                        new[]
                        {
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression($"return ((char)value).ToString()")),
                        });

                    break;

                case TypeModelCategory.List:
                    var itemIdentifier = typeModel.Identifier;
                    var listIdentifier = typeModel.FullIdentifier;
                    var dehumanizedItem = itemIdentifier.Dehumanize();

                    yield return SyntaxFactoryService.ExtensionMethod(
                        listIdentifier,
                        $"To{ListTypeName}{dehumanizedItem}",
                        new[] { (dataTypeModel.Identifier, "value"), ("BaseObject", "baseObject"), },
                        $"return string.IsNullOrEmpty(value) || string.Equals(value, \"_\", StringComparison.Ordinal) ? Array.Empty<{itemIdentifier}>() : value.Split(',').Select(x => x.To{dehumanizedItem}(baseObject)).ToArray()");

                    yield return SyntaxFactoryService.ExtensionMethod(
                        dataTypeModel.Identifier,
                        "ToRaw",
                        new[] { (listIdentifier, "list"), ("int?", "minValue"), ("int?", "maxValue"), },
                        "return (!maxValue.HasValue || list.Count() <= maxValue.Value) ? $\"{string.Join(',', list.Select(value => value.ToRaw(null, null)))}\" : throw new ArgumentOutOfRangeException(nameof(list))");

                    break;

                case TypeModelCategory.EnumInt:
                case TypeModelCategory.EnumFlags:
                default:
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

                    var minMaxValueType = underlyingType == ObjectDataType.String ? "int" : dataTypeModel.Identifier;
                    yield return SyntaxFactoryService.ExtensionMethod(
                        dataTypeModel.Identifier,
                        "ToRaw",
                        new[] { (typeModel.Identifier, "value"), ($"{minMaxValueType}?", "minValue"), ($"{minMaxValueType}?", "maxValue"), },
                        new[]
                        {
                            SyntaxFactory.ExpressionStatement(
                                SyntaxFactory.ParseExpression(
                                    underlyingType == ObjectDataType.Int || underlyingType == ObjectDataType.Char
                                        ? $"return ({dataTypeModel.Identifier})value"
                                        : $"return value.Key.{nameof(Int32Extensions.ToRawcode)}()")),
                        });

                    break;
            }
        }

        internal static bool ParseBool(this object obj)
        {
            return obj is int @int ? @int != 0 : (bool)obj;
        }
    }
}