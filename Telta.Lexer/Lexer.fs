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
    | PunctuationOperator of List<char>
    | Comment

type Lexer(source:SourceFile) =
    member private this.prepareText (lexemes:List<char>) =
        let rec iterLexemes (list:List<char>) builder =
            if list.IsEmpty then builder().ToString()
            else iterLexemes list.Tail (fun() ->
                StringBuilder(list.Head.ToString()).Append(builder().ToString()))
        iterLexemes lexemes (fun() -> StringBuilder())
    
    member private this.getLocation (text:string) =
        let start = Position(
            source.CurrentPosition.Line,
            source.CurrentPosition.Column - text.Length - 1)
        let range = Range(start, text.Length)
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
        | l when System.Char.IsLetter(l) || l = '_' -> Identifier(List.Empty)
        | l when System.Char.IsDigit(l) -> Number(List.Empty, false)
        | l when l = '/' && (match source.ReadChar() with | Char value -> value = '/' | _ -> false) -> Comment
        | '"' -> StringLiteral(List.Empty)
        | ''' -> CharLiteral(List.Empty)
        | l when System.Char.IsSymbol(l) -> Operator(List.Empty)
        | l when System.Char.IsPunctuation(l) -> PunctuationOperator(List.Empty)
        | _ -> Start
    
    member this.Tokenization : TokenStream =
        let tokenStream = TokenStream()
        
        let gotoByLexeme stateMachine (lexeme:Lexeme) =
            match lexeme with
            | Char(value) -> stateMachine (this.getState(value)) lexeme
            | Lexeme.End -> stateMachine Start Lexeme.End
          
        let rec next (state:ReadingState) (lexeme:Lexeme) =
            match lexeme with
            | Char(value) ->
                match state with
                | Start ->
                    if source.ReadChar() = Lexeme.End then
                            tokenStream.AddToken(this.makeToken([value]))
                            next Start Lexeme.End
                    else match value with
                            | v when System.Char.IsWhiteSpace v || System.Char.IsControl v ->
                                gotoByLexeme next (source.ReadAndMove())
                            | _ -> gotoByLexeme next lexeme
                | Identifier(lexemes) ->
                    match value with
                    | v when System.Char.IsLetterOrDigit(v) ->
                        next (Identifier(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        gotoByLexeme next lexeme
                | StringLiteral(lexemes) ->
                    match value with
                    | v when v = '"' && lexemes.Length > 0 ->
                        tokenStream.AddToken(this.makeToken(List<char>.Cons('"', lexemes)))
                        gotoByLexeme next (source.ReadAndMove())
                    | v -> next (StringLiteral(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                | CharLiteral(lexemes) ->
                    match value with
                    | v when v = ''' && lexemes.Length > 0 ->
                        tokenStream.AddToken(this.makeToken(List<char>.Cons(''', lexemes)))
                        gotoByLexeme next (source.ReadAndMove())
                    | v -> next (CharLiteral(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                | Number(lexemes, isReal) ->
                    match value with
                    | v when System.Char.IsDigit(v) ->
                        next (Number(List<char>.Cons(v, lexemes), isReal)) (source.ReadAndMove())
                    | v when (v = '.' || v = ',') && not isReal ->
                        next (Number(List<char>.Cons(v, lexemes), true)) (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        gotoByLexeme next lexeme
                | Operator(lexemes) ->
                    match value with
                    | v when System.Char.IsSymbol(v) ->
                        next (Operator(List<char>.Cons(v, lexemes))) (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        gotoByLexeme next lexeme
                | PunctuationOperator(lexemes) ->
                    match value with
                    | v when System.Char.IsPunctuation v ->
                        tokenStream.AddToken(this.makeToken(List<char>.Cons(v, List.Empty)))
                        gotoByLexeme next (source.ReadAndMove())
                    | _ ->
                        tokenStream.AddToken(this.makeToken(lexemes))
                        gotoByLexeme next lexeme
                | Comment ->
                    let rec skip (lexeme:Lexeme) =
                        match lexeme with
                        | Char value when value = '\n' -> gotoByLexeme next (source.ReadAndMove())
                        | Lexeme.End -> next Start Lexeme.End
                        | _ -> skip (source.ReadAndMove())
                    skip lexeme
                                                     
            | Lexeme.End ->
                tokenStream.AddToken(this.makeToken(List.Empty, TokenType.End))
                
        next ReadingState.Start (source.ReadAndMove())
        tokenStream
