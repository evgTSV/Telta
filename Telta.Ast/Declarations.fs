namespace Telta.Ast

open Telta.Ast.Statements
open Telta.Lexer

module Declarations =
    
    type Declaration =
        | NamespaceDeclaration of identifier:Token * boundedDeclarations:List<Declaration>
        | ParameterDeclaration of identifier:Token * predefinedType:Token
        | MethodDeclaration of identifier:Token * modifiers:List<Token> * parameters:List<Declaration> * predefinedType:Token * block:Statement 