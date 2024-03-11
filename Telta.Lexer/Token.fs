namespace Telta.Lexer

open Telta.Syntax

type Token(
    tokenType:TokenType,
    location:Location,
    text:string) =
    member this.Type : TokenType = tokenType
    member this.Location : Location = location
    member this.Text : string = text