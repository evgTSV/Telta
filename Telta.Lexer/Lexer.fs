namespace Telta.Lexer

open System.Text
open Telta.Syntax

type ReadingState =
    | Start
    | Identifier
    | StringLiteral
    | CharLiteral
    | Number
    | Operator
    | Comment
    | End

type Lexer(source:SourceFile) =
    member val private currentPosition = Position(0, 0)
    
    member private this.getLocationAndMove (text:string) =
        let range = Range(this.currentPosition, text.Length)
        Location(source, range)
        
    member private this.makeToken (text:string) =
        let ``type`` = TokenRegexes.findMatchToken text
        let location = this.getLocationAndMove text
        Token(``type``, location, text)  
    
    member this.Tokenization : TokenStream =
        raise(System.NotImplementedException())