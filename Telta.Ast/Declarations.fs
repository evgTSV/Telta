namespace Telta.Ast

open Telta.Ast.Expressions
open Telta.Ast.Statements
open Telta.Lexer

module Declarations =
    
    type Declaration =
        | NamespaceDeclaration of identifier:Token * boundedDeclarations:List<Declaration> * semicolon:Token
        | TypeDeclaration of identifier:Token * members:List<Declaration> * modifiers:List<Token>
        | FieldDeclaration of modifiers:List<Token> * variableDeclarator:Declaration
        | VariableDeclaration of identifier:Token * predefinedType:Token * value:Expression
        | ParameterDeclaration of identifier:Token * predefinedType:Token
        | MethodDeclaration of identifier:Token * modifiers:List<Token> * parameters:List<Declaration> * predefinedType:Token * block:Statement