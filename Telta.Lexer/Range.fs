namespace Telta.Lexer

open System

type public Range =
    class
        val _start:Position
        val _end:Position
        
        new (start : Position, ``end`` : Position) =
            if start > ``end`` then raise (ArgumentException("The start cannot be greater than end"))
            {
                _start = start
                _end = ``end``
            }
        
        member this.Start with get() = this._start
        member this.End with get() = this._end
        member this.Length with get() = (this.End.Line - this.Start.Line + 1) * (this.End.Column - this.Start.Column)
    end