namespace Telta.Lexer

open Telta.Syntax

type Token = { TokenType:TokenType; Location:Location; Text:string }