namespace Telta.Ast

open Telta.Ast.Expressions
open Telta.Lexer

module Statements =
    
    type Statement =
        | IfElseStatement of ifExpression:Expression * elseExpression:Expression
        | GotoStatement of identifier:Token * semicolon:Token
        | ReturnStatement of value:Expression * semicolon:Token
        | UsingStatement of identifier:Token * semicolon:Token
        | CompoundStatement of statements:Statement list * parentheses:(Token * Token)