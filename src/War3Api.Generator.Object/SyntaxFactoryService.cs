// ------------------------------------------------------------------------------
// <copyright file="SyntaxFactoryService.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace War3Api.Generator.Object
{
    internal static class SyntaxFactoryService
    {
        internal static int GetOrder(MemberDeclarationSyntax member)
        {
            var modifierValue = 0;
            foreach (var modifier in member.Modifiers)
            {
                modifierValue += modifier.Kind() switch
                {
                    SyntaxKind.PrivateKeyword => 1 << 0,
                    SyntaxKind.ProtectedKeyword => 1 << 1,
                    SyntaxKind.InternalKeyword => 1 << 2,
                    SyntaxKind.PublicKeyword => 1 << 3,
                    SyntaxKind.ReadOnlyKeyword => 1 << 4,
                    SyntaxKind.StaticKeyword => 1 << 5,
                    SyntaxKind.ConstKeyword => 1 << 6,
                    SyntaxKind.ImplicitKeyword => 1 << 7,
                    SyntaxKind.ExplicitKeyword => 1 << 8,
                };
            }

            if (member is MethodDeclarationSyntax)
            {
                return modifierValue;
            }

            if (member is ConversionOperatorDeclarationSyntax)
            {
                return modifierValue + (1 << 9);
            }

            if (member is OperatorDeclarationSyntax)
            {
                return modifierValue + (1 << 10);
            }

            if (member is PropertyDeclarationSyntax)
            {
                return modifierValue + (1 << 11);
            }

            if (member is ConstructorDeclarationSyntax)
            {
                return modifierValue + (1 << 12);
            }

            if (member is FieldDeclarationSyntax)
            {
                return modifierValue + (1 << 13);
            }

            throw new NotSupportedException(member.GetType().FullName);
        }

        internal static ClassDeclarationSyntax Class(string identifier, bool isAbstract, string? baseType, IEnumerable<MemberDeclarationSyntax> members)
        {
            return Class(identifier, isAbstract ? SyntaxKind.AbstractKeyword : SyntaxKind.SealedKeyword, baseType, members);
        }

        internal static ClassDeclarationSyntax Class(string identifier, SyntaxKind keyword, string? baseType, IEnumerable<MemberDeclarationSyntax> members)
        {
            BaseListSyntax baseList = null;
            if (!string.IsNullOrWhiteSpace(baseType))
            {
                baseList = SyntaxFactory.BaseList(SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseType))));
            }

            return SyntaxFactory.ClassDeclaration(
                default,
                new SyntaxTokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(keyword)),
                SyntaxFactory.Identifier(identifier),
                null,
                baseList,
                default,
                new SyntaxList<MemberDeclarationSyntax>(members.OrderByDescending(member => GetOrder(member))));
        }

        internal static FieldDeclarationSyntax Field(string type, string identifier, bool isReadonly)
        {
            var tokenList = isReadonly
                ? SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
                    SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword))
                : SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));

            return SyntaxFactory.FieldDeclaration(
                default,
                tokenList,
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName(type),
                    default(SeparatedSyntaxList<VariableDeclaratorSyntax>).Add(
                        SyntaxFactory.VariableDeclarator(identifier))));
        }

        internal static FieldDeclarationSyntax Field(string type, string identifier, ExpressionSyntax expression, SyntaxKind accessModifier = SyntaxKind.PrivateKeyword)
        {
            return SyntaxFactory.FieldDeclaration(
                default,
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(accessModifier),
                    SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword)),
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName(type),
                    default(SeparatedSyntaxList<VariableDeclaratorSyntax>).Add(
                        SyntaxFactory.VariableDeclarator(
                            SyntaxFactory.Identifier(identifier),
                            null,
                            SyntaxFactory.EqualsValueClause(expression)))));
        }

        internal static ConstructorDeclarationSyntax Constructor(
            SyntaxKind accessModifier,
            string identifier,
            IEnumerable<(string type, string identifier)> parameters)
        {
            return SyntaxFactory.ConstructorDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(accessModifier)),
                SyntaxFactory.Identifier(identifier),
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Parameter(
                                default,
                                default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                ConstructorInitializer(true, parameters),
                SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>()),
                null);
        }

        internal static ConstructorDeclarationSyntax Constructor(
            SyntaxKind accessModifier,
            string identifier,
            bool callsThisInitializer,
            IEnumerable<string> initializerValues,
            IEnumerable<(string type, string identifier)> parameters)
        {
            return SyntaxFactory.ConstructorDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(accessModifier)),
                SyntaxFactory.Identifier(identifier),
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Parameter(
                                default,
                                default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                ConstructorInitializer(callsThisInitializer, initializerValues.Select(val => (val, false)).Concat(parameters.Select(param => (param.identifier, false)))),
                SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>()),
                null);
        }

        internal static ConstructorDeclarationSyntax Constructor(
            SyntaxKind accessModifier,
            string identifier,
            IEnumerable<(string type, string identifier)> parameters,
            IEnumerable<(string field, ExpressionSyntax expression)> assignments)
        {
            return SyntaxFactory.ConstructorDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(accessModifier)),
                SyntaxFactory.Identifier(identifier),
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Parameter(
                                default,
                                default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                ConstructorInitializer(false, parameters),
                SyntaxFactory.Block(
                    SyntaxFactory.List<StatementSyntax>(
                        assignments.Select(assignment =>
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.ParseExpression(assignment.field),
                                assignment.expression))))),
                null);
        }

        internal static ConstructorDeclarationSyntax Constructor(
            SyntaxKind accessModifier,
            string identifier,
            bool callsThisInitializer,
            IEnumerable<string> initializerValues,
            IEnumerable<(string type, string identifier)> parameters,
            IEnumerable<(string field, ExpressionSyntax expression)> assignments)
        {
            return SyntaxFactory.ConstructorDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(accessModifier)),
                SyntaxFactory.Identifier(identifier),
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Parameter(
                                default,
                                default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                ConstructorInitializer(callsThisInitializer, initializerValues.Select(val => (val, false)).Concat(parameters.Select(param => (param.identifier, false)))),
                SyntaxFactory.Block(
                    SyntaxFactory.List<StatementSyntax>(
                        assignments.Select(assignment =>
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.ParseExpression(assignment.field),
                                assignment.expression))))),
                null);
        }

        internal static ConstructorInitializerSyntax ConstructorInitializer(bool @this, IEnumerable<(string type, string identifier)> parameters)
        {
            return ConstructorInitializer(
                @this ? SyntaxKind.ThisConstructorInitializer : SyntaxKind.BaseConstructorInitializer,
                @this ? SyntaxKind.ThisKeyword : SyntaxKind.BaseKeyword,
                parameters.Select(param => (param.identifier, param.type.EndsWith("type", StringComparison.OrdinalIgnoreCase))));
        }

        internal static ConstructorInitializerSyntax ConstructorInitializer(bool @this, IEnumerable<(string identifier, bool castToInt)> parameters)
        {
            return ConstructorInitializer(
                @this ? SyntaxKind.ThisConstructorInitializer : SyntaxKind.BaseConstructorInitializer,
                @this ? SyntaxKind.ThisKeyword : SyntaxKind.BaseKeyword,
                parameters);
        }

        private static ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind initializer, SyntaxKind keyword, IEnumerable<(string identifier, bool castToInt)> parameters)
        {
            return SyntaxFactory.ConstructorInitializer(
                initializer,
                SyntaxFactory.Token(SyntaxKind.ColonToken),
                SyntaxFactory.Token(keyword),
                SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Argument(
                                SyntaxFactory.ParseExpression($"{(parameter.castToInt ? "(int)" : string.Empty)}{parameter.identifier}"))))));
        }

        internal static ObjectCreationExpressionSyntax ObjectCreation(string type, params ExpressionSyntax[] arguments)
        {
            return SyntaxFactory.ObjectCreationExpression(
                SyntaxFactory.ParseTypeName(type),
                SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments.Select(argument => SyntaxFactory.Argument(argument)))),
                null);
        }

        internal static PropertyDeclarationSyntax Property(string type, string identifier, ExpressionSyntax getter)
        {
            return SyntaxFactory.PropertyDeclaration(
                default,
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                SyntaxFactory.ParseTypeName(type),
                null,
                SyntaxFactory.Identifier(identifier),
                null,
                SyntaxFactory.ArrowExpressionClause(getter),
                null,
                SyntaxFactory.Token(SyntaxKind.SemicolonToken));
        }

        internal static PropertyDeclarationSyntax Property(string type, string identifier, AccessorDeclarationSyntax getter, AccessorDeclarationSyntax setter)
        {
            return SyntaxFactory.PropertyDeclaration(
                default,
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                SyntaxFactory.ParseTypeName(type),
                null,
                SyntaxFactory.Identifier(identifier),
                SyntaxFactory.AccessorList(
                    SyntaxFactory.List(new[] { getter, setter })),
                null,
                null);
        }

        internal static AccessorDeclarationSyntax Getter(ExpressionSyntax expression)
        {
            return Accessor(SyntaxKind.GetAccessorDeclaration, SyntaxKind.GetKeyword, expression);
        }

        internal static AccessorDeclarationSyntax Getter(params StatementSyntax[] statements)
        {
            return Accessor(SyntaxKind.GetAccessorDeclaration, SyntaxKind.GetKeyword, statements);
        }

        internal static AccessorDeclarationSyntax Setter(ExpressionSyntax expression)
        {
            return Accessor(SyntaxKind.SetAccessorDeclaration, SyntaxKind.SetKeyword, expression);
        }

        internal static AccessorDeclarationSyntax Setter(params StatementSyntax[] statements)
        {
            return Accessor(SyntaxKind.SetAccessorDeclaration, SyntaxKind.SetKeyword, statements);
        }

        private static AccessorDeclarationSyntax Accessor(SyntaxKind accessor, SyntaxKind keyword, ExpressionSyntax expression)
        {
            return SyntaxFactory.AccessorDeclaration(
                accessor,
                default,
                default,
                SyntaxFactory.Token(keyword),
                null,
                SyntaxFactory.ArrowExpressionClause(expression),
                SyntaxFactory.Token(SyntaxKind.SemicolonToken));
        }

        private static AccessorDeclarationSyntax Accessor(SyntaxKind accessor, SyntaxKind keyword, params StatementSyntax[] statements)
        {
            return SyntaxFactory.AccessorDeclaration(
                accessor,
                default,
                default,
                SyntaxFactory.Token(keyword),
                SyntaxFactory.Block(statements),
                null,
                SyntaxFactory.Token(SyntaxKind.None));
        }

        internal static MethodDeclarationSyntax Method(
            string returnType,
            string identifier,
            IEnumerable<(string type, string identifier)> parameters,
            IEnumerable<StatementSyntax> statements)
        {
            return Method(SyntaxKind.PrivateKeyword, false, returnType, identifier, parameters, statements);
        }

        internal static MethodDeclarationSyntax Method(
            SyntaxKind accessModifierKeyword,
            bool isStatic,
            string returnType,
            string identifier,
            IEnumerable<(string type, string identifier)> parameters,
            IEnumerable<StatementSyntax> statements)
        {
            var tokenList = new List<SyntaxToken>
            {
                SyntaxFactory.Token(accessModifierKeyword),
            };

            if (isStatic)
            {
                tokenList.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            }

            return SyntaxFactory.MethodDeclaration(
                default,
                SyntaxFactory.TokenList(tokenList),
                SyntaxFactory.ParseTypeName(returnType),
                null,
                SyntaxFactory.Identifier(identifier),
                null,
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Parameter(
                                default,
                                default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                default,
                SyntaxFactory.Block(SyntaxFactory.List(statements)),
                null);
        }

        internal static MethodDeclarationSyntax ExtensionMethod(
            string returnType,
            string identifier,
            IEnumerable<(string type, string identifier)> parameters,
            IEnumerable<StatementSyntax> statements)
        {
            return SyntaxFactory.MethodDeclaration(
                default,
                new SyntaxTokenList(
                    SyntaxFactory.Token(SyntaxKind.InternalKeyword),
                    SyntaxFactory.Token(SyntaxKind.StaticKeyword)),
                SyntaxFactory.ParseTypeName(returnType),
                null,
                SyntaxFactory.Identifier(identifier),
                null,
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select((parameter, index) =>
                            SyntaxFactory.Parameter(
                                default,
                                index == 0 ? new SyntaxTokenList(SyntaxFactory.Token(SyntaxKind.ThisKeyword)) : default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                default,
                SyntaxFactory.Block(SyntaxFactory.List(statements)),
                null);
        }

        internal static MethodDeclarationSyntax ExtensionMethod(
            string returnType,
            string identifier,
            IEnumerable<(string type, string identifier)> parameters,
            string expressionStatement)
        {
            return SyntaxFactory.MethodDeclaration(
                default,
                new SyntaxTokenList(
                    SyntaxFactory.Token(SyntaxKind.InternalKeyword),
                    SyntaxFactory.Token(SyntaxKind.StaticKeyword)),
                SyntaxFactory.ParseTypeName(returnType),
                null,
                SyntaxFactory.Identifier(identifier),
                null,
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select((parameter, index) =>
                            SyntaxFactory.Parameter(
                                default,
                                index == 0 ? new SyntaxTokenList(SyntaxFactory.Token(SyntaxKind.ThisKeyword)) : default,
                                SyntaxFactory.ParseTypeName(parameter.type),
                                SyntaxFactory.Identifier(parameter.identifier),
                                null)))),
                default,
                SyntaxFactory.Block(SyntaxFactory.SingletonList((StatementSyntax)SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression(expressionStatement)))),
                null);
        }
    }
}