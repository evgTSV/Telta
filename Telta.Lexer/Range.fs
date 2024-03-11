namespace Telta.Lexer

open System

type public Range =
    class
        val _start:Position
        val _end:Position
        
        new (start : Position, ``end`` : Position) =
            if start > ``end`` then raise (ArgumentException("The start cannot be greater than end", nameof(``end``)))
            {
                _start = start
                _end = ``end``
            }
        new (start : Position, length : int32) =
            if length < 0 then raise (ArgumentException("Length cannot be negative", nameof(length)))
            {
                _start = start
                _end = Position(start.Line, start.Column + length)
            }
        
        member this.Start with get() = this._start
        member this.End with get() = this._end
        member this.Length with get() = (this.End.Line - this.Start.Line + 1) * (this.End.Column - this.Start.Column)
    end