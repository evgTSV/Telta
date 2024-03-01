namespace Telta.Syntax

type TokenType =
    // Specials
    /// Empty or null
    | Empty 
    /// End of source
    | End
    /// Unrecognized
    | Unknown
    
    // Brackets
    /// '('
    | OpenParen
    /// ')'
    | CloseParen
    /// '['
    | OpenBracket
    /// ']'
    | CloseBracket
    /// '<'
    | OpenAngleBracket
    /// '>'
    | CloseAngleBracket
    /// '{'
    | OpenBrace
    /// '}'
    | CloseBrace
    
    // Logical operators
    /// '=='
    | Equal
    /// '!='
    | NotEqual
    /// '&&'
    | LogicalAnd
    /// '||'
    | LogicalOr
    /// '!'
    | LogicalNot
    
    // Increment and Decrement
    /// '+|'
    | IncrementPrefix
    /// '|+'
    | IncrementPostfix
    /// '-|'
    | DecrementPrefix
    /// '|-'
    | DecrementPostfix
    
    // Standard math operations
    /// '+'
    | Add
    /// '+='
    | AddAssign
    /// '-'
    | Subtract
    /// '-='
    | SubtractAssign
    /// '*'
    | Multiple
    /// '*='
    | MultipleAssign
    /// '/'
    | Divide
    /// '/='
    | DivideAssign
    /// '%'
    | Module
    /// '%='
    | ModuleAssign
    /// '**'
    | Pow
    /// '**='
    | PowAssign

    // Bits operations
    /// '&'
    | BitAnd
    /// '&='
    | BitAndAssign
    /// <summary> '|' </summary>
    | BitOr
    /// '|='
    | BitOrAssign
    /// <summary> '^' </summary>
    | BitXor
    /// '^='
    | BitXorAssign
    /// '~'
    | BitNot
    /// '<<'
    | ShiftLeft
    /// '<<='
    | ShiftLeftAssign
    /// '>>'
    | ShiftRight
    /// '>>='
    | ShiftRightAssign
    
    // Others operators
    /// '->'
    | Array
    /// '.'
    | Dot
    /// ','
    | Comma
    /// ':'
    | Colon
    /// ';'
    | Semicolon
    /// '='
    | Assign
    /// '`'
    | Backquote
    /// '''
    | QuotationMark
    /// '"'
    | DoubleQuotationMark
    
    // Keywords