namespace Telta.Ast

open Telta.Ast.Expressions
open Telta.Lexer

module Statements =
    
    type Statement =
        | IfElseStatement of ifExpression:Expression * elseExpression:Expression
        | GotoStatement of identifier:Token
        | ReturnStatement of value:Expression
        | UsingStatement of identifier:Token
        | CompoundStatement of statements:Statement