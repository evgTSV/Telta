namespace Telta.Lexer.Tests

open Telta.Lexer
open System.IO

type FakeSource(code:StringReader) =
    inherit SourceFile("null")
    override b.ReadChar() =
        b.lastRead
    override b.Move() = b.ReadAndMove |> ignore
    override b.ReadAndMove() =
        let c = code.Read()
        if c = -1 then b.lastRead <- End
        else
            b.lastRead <- Char(char c)
            if (char c) = '\n' then b.pos <- Position(b.pos.Line + 1, 0)
            else b.pos <- Position(b.pos.Line, b.pos.Column + 1)
        b.lastRead
        
    override this.ResetPosition() = failwith "todo"
    override this.CurrentPosition = this.pos
        
    member val pos:Position = Position(0,0) with get, set
    member val lastRead:Lexeme = Char(char(code.Peek())) with get, set