namespace Telta.Lexer

type Lexeme =
    | Char of value:char
    | End

[<AbstractClass>]
type SourceFile(fileName:string) =
    abstract member ReadAndMove : unit -> Lexeme
    abstract member ReadChar : unit -> Lexeme
    abstract member Move : unit -> unit
    abstract member ResetPosition : unit -> unit
    abstract member CurrentPosition : Position with get
    member this.FileName with get() = fileName