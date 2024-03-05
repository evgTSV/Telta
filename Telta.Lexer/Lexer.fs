namespace Telta.Lexer

type ReadingState =
    | Start
    | Identifier
    | StringLiteral
    | CharLiteral
    | Number
    | Operator
    | Comment
    | Error
    | End

// type Lexer =
    