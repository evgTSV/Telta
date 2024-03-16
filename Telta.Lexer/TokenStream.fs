namespace Telta.Lexer

open System.Collections.Generic
open System.Collections

type TokenStream() =
    interface IEnumerable<Token> with
        member this.GetEnumerator() = (this.tokens :> IEnumerable<Token>).GetEnumerator()
        member this.GetEnumerator() = (this.tokens :> IEnumerable).GetEnumerator()
    member val tokens = ResizeArray<Token>()
    member val public Position = -1 with get,set
    member public this.AddToken (token:Token) = this.tokens.Add(token)
    member public this.Length with get() = this.tokens.Count
    member public this.Read =
        if this.Position < this.Length - 1 then          
            this.Position <- this.Position + 1
            this.tokens[this.Position]
        else this.tokens[this.Position]