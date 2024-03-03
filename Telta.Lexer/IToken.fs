namespace Telta.Lexer

open Telta.Syntax

[<Interface>]
type IToken =
    abstract member Type : TokenType
        with get
    abstract member Location : Location
    abstract member Text : string
        with get