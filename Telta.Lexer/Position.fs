namespace Telta.Lexer

open System

[<Struct>]
[<CustomEquality>]
[<CustomComparison>]
type public Position(line:int, column:int) =
    interface IComparable<Position> with
        member this.CompareTo(other:Position) =
            let lineCompare = this.Line - other.Line
            if lineCompare = 0 then this.Column - other.Column
            else lineCompare
    interface IComparable with
        member this.CompareTo(other) =
            match other with
            | :? Position as pos -> (this :> IComparable<Position>).CompareTo(pos)
            | _ -> raise (ArgumentException($"Argument must be type of {typeof<Position>.FullName}", nameof(other)))
    member this.Line with get() = line
    member this.Column with get() = column
    
    member this.CompareTo(other:Position) = (this :> IComparable<Position>).CompareTo(other)
    member this.CompareTo(other) = (this :> IComparable).CompareTo(other)
    override this.Equals(other) = (this :> IComparable).CompareTo(other) = 0
    override this.GetHashCode() = line ^^^ column
    
    static member op_Equality (pos1:Position, pos2:Position) = (pos1.CompareTo(pos2) = 0)
    static member op_Inequality (pos1:Position, pos2:Position) = (pos1.CompareTo(pos2) <> 0)
    static member op_GreaterThan (pos1:Position, pos2:Position) = (pos1.CompareTo(pos2) > 0)
    static member op_LessThan (pos1:Position, pos2:Position) = (pos1.CompareTo(pos2) < 0)