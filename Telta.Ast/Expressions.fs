namespace Telta.Ast

open Telta.Lexer

module Expressions =

    type Expression =
        | IdentifierExpression of identifier:Token
        | ParenthesizedExpression of contents:Expression
        | InvocationExpression of ``member``:Expression * arguments:List<Expression>
        | MemberAccessExpression of target:Expression * identifier:Expression
        | ObjectCreationExpression of identifier:Token * arguments:List<Expression>
        | SimpleAssignExpression of ``member``:Expression * value:Expression
        | StringLiteralExpression of value:Token
        | NumericLiteralExpression of value:Token
        | BinaryExpression of operator:Token * left:Expression * right:Expression
        | Empty