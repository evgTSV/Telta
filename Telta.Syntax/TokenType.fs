namespace Telta.Syntax

type TokenType =
    // Specials
    /// <summary> Empty or null </summary>
    | Empty
    /// <summary> End of source </summary>
    | End
    /// <summary> Unrecognized </summary>
    | Unknown
    
    // Logical operators
    /// <summary> '==' </summary>
    | Equal
    /// <summary> '!=' </summary>
    | NotEqual
    /// <summary> '&&' </summary>
    | LogicalAnd
    /// <summary> '||' </summary>
    | LogicalOr
    /// <summary> '!' </summary>
    | LogicalNot
    
    // Increment and Decrement
    /// <summary> '+|' </summary>
    | IncrementPre
    /// <summary> '|+' </summary>
    | IncrementPost
    /// <summary> '-|' </summary>
    | DecrementPre
    /// <summary> '|-' </summary>
    | DecrementPost
    
    // Standard math operations
    // Increment and Decrement
    /// <summary> '+' </summary>
    | Add
    /// <summary> '|+|' </summary>
    | AddAssign
    /// <summary> '-' </summary>
    | Subtract
    /// <summary> '|-|' </summary>
    | SubtractAssign
    /// <summary> '*' </summary>
    | Multiple
    /// <summary> '|*|' </summary>
    | MultipleAssign
    /// <summary> '/' </summary>
    | Divide
    /// <summary> '|/|' </summary>
    | DivideAssign
    /// <summary> '%' </summary>
    | Module
    /// <summary> '|%|' </summary>
    | ModuleAssign

    // Bits operations
    /// <summary> '&' </summary>
    | BitAnd
    /// <summary> '|&|' </summary>
    | BitAndAssign
    /// <summary> '|' </summary>
    | BitOr
    /// <summary> '|||' </summary>
    | BitOrAssign
    /// <summary> '^' </summary>
    | BitXor
    /// <summary> '|^|' </summary>
    | BitXorAssign
    /// <summary> '~|' </summary>
    | BitNot