namespace Telta.Lexer

[<AbstractClass>]
type SourceFile(fileName:string) =
    abstract member ReadLine : unit -> bool
    abstract member CurrentLine : string with get
    member this.FileName with get() = fileName