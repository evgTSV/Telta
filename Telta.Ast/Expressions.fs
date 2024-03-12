namespace Telta.Ast

open Telta.Lexer

module Expressions =

    type Expression =
        | IdentifierExpression of identifier:string
        | ParenExpression of contents:Token
        | MemberCallExpression of ``member``:Expression * arguments:List<Expression>
        | MemberAccessExpression of target:Expression * identifier:Expression