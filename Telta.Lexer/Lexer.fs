namespace Telta.Lexer

open System.Text

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
    let currentLexeme = StringBuilder()
    member val private currentLine = 0
    member val private currentColumn = 0