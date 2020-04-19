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
                    SyntaxKind.PrivateKeyword => 0,
                    SyntaxKind.ProtectedKeyword => 1,
                    SyntaxKind.InternalKeyword => 2,
                    SyntaxKind.PublicKeyword => 3,
                    SyntaxKind.ReadOnlyKeyword => 4,
                    SyntaxKind.StaticKeyword => 8,
                    SyntaxKind.ConstKeyword => 16,
                };
            }

            if (member is MethodDeclarationSyntax)
            {
                return modifierValue;
            }

            if (member is PropertyDeclarationSyntax)
            {
                return modifierValue + 32;
            }

            if (member is ConstructorDeclarationSyntax)
            {
                return modifierValue + 64;
            }

            if (member is FieldDeclarationSyntax)
            {
                return modifierValue + 128;
            }

            throw new NotSupportedException();
        }

        internal static ClassDeclarationSyntax Class(string identifier, bool isAbstract, string? baseType, IEnumerable<MemberDeclarationSyntax> members)
        {
            return SyntaxFactory.ClassDeclaration(
                default,
                new SyntaxTokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(isAbstract ? SyntaxKind.AbstractKeyword : SyntaxKind.SealedKeyword)),
                SyntaxFactory.Identifier(identifier),
                null,
                SyntaxFactory.BaseList(
                    string.IsNullOrWhiteSpace(baseType)
                        ? SyntaxFactory.SeparatedList<BaseTypeSyntax>()
                        : SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseType)))),
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

        internal static FieldDeclarationSyntax Field(string type, string identifier, ExpressionSyntax expression)
        {
            return SyntaxFactory.FieldDeclaration(
                default,
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
                    SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword)),
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName(type),
                    default(SeparatedSyntaxList<VariableDeclaratorSyntax>).Add(
                        SyntaxFactory.VariableDeclarator(
                            SyntaxFactory.Identifier(identifier),
                            null,
                            SyntaxFactory.EqualsValueClause(expression)))));
        }

        internal static ConstructorDeclarationSyntax Constructor(string identifier, IEnumerable<(string type, string identifier)> parameters)
        {
            return SyntaxFactory.ConstructorDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
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
                ConstructorInitializer(SyntaxKind.ThisConstructorInitializer, SyntaxKind.ThisKeyword, parameters),
                SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>()),
                null);
        }

        internal static ConstructorDeclarationSyntax Constructor(
            string identifier,
            IEnumerable<(string type, string identifier)> parameters,
            IEnumerable<(string field, ExpressionSyntax expression)> assignments)
        {
            return SyntaxFactory.ConstructorDeclaration(
                default,
                SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.PrivateKeyword)),
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
                ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, SyntaxKind.BaseKeyword, parameters),
                SyntaxFactory.Block(
                    SyntaxFactory.List<StatementSyntax>(
                        assignments.Select(assignment =>
                            SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.ParseExpression(assignment.field),
                                assignment.expression))))),
                null);
        }

        private static ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind initializer, SyntaxKind keyword, IEnumerable<(string type, string identifier)> parameters)
        {
            return SyntaxFactory.ConstructorInitializer(
                initializer,
                SyntaxFactory.Token(SyntaxKind.ColonToken),
                SyntaxFactory.Token(keyword),
                SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList(
                        parameters.Select(parameter =>
                            SyntaxFactory.Argument(
                                SyntaxFactory.ParseExpression(
                                    parameter.type == SyntaxFactory.Token(SyntaxKind.IntKeyword).ValueText ||
                                    parameter.type == SyntaxFactory.Token(SyntaxKind.StringKeyword).ValueText
                                        ? parameter.identifier
                                        : $"(int){parameter.identifier}"))))));
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
            return SyntaxFactory.MethodDeclaration(
                default,
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PrivateKeyword)),
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
    }
}