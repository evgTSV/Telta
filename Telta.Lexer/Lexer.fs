namespace Telta.Lexer

open System.Text
open Telta.Syntax

type ReadingState =
    | Start
    | Identifier of List<char>
    | StringLiteral of List<char>
    | CharLiteral of List<char>
    | Number of List<char> * isReal:bool
    | Operator of List<char>
    | Comment

type Lexer(source:SourceFile) =
    member private this.prepareText (lexemes:List<char>) =
        let sb = StringBuilder()
        lexemes |> Seq.iter (fun l -> sb.Append(l) |> ignore)
        sb.ToString()
    
    member private this.getLocation (text:string) =
        let range = Range(source.CurrentPosition, text.Length)
        Location(source, range)
        
    member private this.makeToken (lexemes:List<char>, ?tokenType:TokenType) =
        let text = this.prepareText lexemes
        let ``type`` =
            if tokenType.IsSome then tokenType.Value
            else TokenRegexes.findMatchToken text
        let location = this.getLocation text
        Token(``type``, location, text)
        
    member private this.getState (lexeme:char) : ReadingState =
        match lexeme with
        | l when System.Char.IsLetter(l) || l = '_' -> Identifier(List<char>.Cons(lexeme, List.Empty))
        | l when System.Char.IsDigit(l) -> Number(List<char>.Cons(lexeme, List.Empty), false)
        | l when l = '/' && (match source.ReadChar() with | Char value -> value = '/' | _ -> false) -> Comment
        | '"' -> StringLiteral(List<char>.Cons(lexeme, List.Empty))
        | ''' -> CharLiteral(List<char>.Cons(lexeme, List.Empty))
        | _ -> Operator(List<char>.Cons(lexeme, List.Empty))
    
    member this.Tokenization : TokenStream =
        let tokenStream = TokenStream()
        let rec next (state:ReadingState) (lexeme:Lexeme) =
            match lexeme with
            | Char(value) ->
                match state with
                | Start ->
                    match value with
                    | v when System.String.IsNullOrWhiteSpace(v.ToString()) ->
                        next ReadingState.Start (source.ReadAndMove())
                    | v -> next (this.getState(v)) (source.ReadChar()) 
                | Identifier(lexemes) ->
                    match value with
                    | v when System.Char.IsLetterOrDigit(v) ->
                        next (Identifier(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        next Start lexeme
                | StringLiteral(lexemes) ->
                    match value with
                    | '"' ->
                        tokenStream.AddToken(this.makeToken(List<char>.Cons('"', lexemes)))
                        next Start lexeme
                    | v -> next (StringLiteral(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                | CharLiteral(lexemes) ->
                    match value with
                    | ''' ->
                        tokenStream.AddToken(this.makeToken(List<char>.Cons(''', lexemes)))
                        next Start lexeme
                    | v -> next (CharLiteral(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                | Number(lexemes, isReal) ->
                    match value with
                    | v when System.Char.IsDigit(v) ->
                        next (Number(List<char>.Cons(v, lexemes), isReal)) (source.ReadAndMove())
                    | v when (v = '.' || v = ',') && not isReal ->
                        next (Number(List<char>.Cons(v, lexemes), true)) (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        next Start lexeme
                | Operator(lexemes) ->
                    match value with
                    | v when System.Char.IsSymbol(v) ->
                        next (Operator(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        next Start lexeme
                | Comment ->
                    while value.ToString() <> System.Environment.NewLine do
                        next Comment (source.ReadAndMove())
                    next Start (source.ReadAndMove())
                    
            | Lexeme.End ->
                tokenStream.AddToken(this.makeToken(List.Empty, End))
                
        next ReadingState.Start (source.ReadAndMove())
        tokenStream
