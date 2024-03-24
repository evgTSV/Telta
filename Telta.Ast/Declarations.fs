namespace Telta.Ast

open Telta.Lexer

module Declarations =
    
    type Declaration =
        | ParameterDeclaration of identifier:Token * predefinedType:Token
        | MethodDeclaration of identifier:Token * modifiers:List<Token> * parameters:List<Declaration>      