namespace Telta.Syntax

type TokenType =
    // Specials
    /// <summary> Empty or null </summary>
    | Empty
    /// <summary> End of source </summary>
    | End
    /// <summary> Unrecognized </summary>
    | Unknown
    
    // Brackets
    /// <summary> '(' </summary>
    | OpenParen
    /// <summary> ')' </summary>
    | CloseParen
    /// <summary> '[' </summary>
    | OpenBracket
    /// <summary> ']' </summary>
    | CloseBracket
    /// <summary> '&lt;' </summary>
    | OpenAngleBracket
    /// <summary> '&gt;' </summary>
    | CloseAngleBracket
    /// <summary> '{' </summary>
    | OpenBrace
    /// <summary> '}' </summary>
    | CloseBrace
    
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
    | IncrementPrefix
    /// <summary> '|+' </summary>
    | IncrementPostfix
    /// <summary> '-|' </summary>
    | DecrementPrefix
    /// <summary> '|-' </summary>
    | DecrementPostfix
    
    // Standard math operations
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
    /// <summary> '**' </summary>
    | Pow
    /// <summary> '|**|' </summary>
    | PowAssign

    // Bits operations
    /// <summary> '&amp;' </summary>
    | BitAnd
    /// <summary> '|&amp;|' </summary>
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
    /// <summary> '&lt;&lt;' </summary>
    | ShiftLeft
    /// <summary> '|&lt;&lt;|' </summary>
    | ShiftLeftAssign
    /// <summary> '&gt;&gt;' </summary>
    | ShiftRight
    /// <summary> '|&gt;&gt;|' </summary>
    | ShiftRightAssign
    
    // Others operators
    /// <summary> '-&gt;' </summary>
    | Array
    /// <summary> '|-&gt;' </summary>
    | TreeArray
    /// <summary> '.' </summary>
    | Dot
    /// <summary> ',' </summary>
    | Comma
    /// <summary> ':' </summary>
    | Colon
    /// <summary> ';' </summary>
    | Semicolon
    
    // Keywords