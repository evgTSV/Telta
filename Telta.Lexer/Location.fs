namespace Telta.Lexer

type Location(source : SourceFile, range : Range) =
    member this.Source with get() = source
    member this.Range with get() = range
    