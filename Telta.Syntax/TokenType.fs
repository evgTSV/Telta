﻿namespace Telta.Syntax

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
    /// '|'
    | BitOr
    /// '|='
    | BitOrAssign
    /// '^'
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
    
    // Literals
    /// string literal
    | StringLiteral
    /// Char literal
    | CharLiteral
    /// Int32 literal
    | IntLiteral
    /// float literal
    | FloatLiteral
    
    // Keywords
    /// 'if' keyword
    | KeywordIf
    /// 'elif' keyword
    | KeywordElif
    /// 'else' keyword
    | KeywordElse
    /// 'goto' keyword
    | KeywordGoto
    /// 'print' keyword
    | KeywordPrint
    /// 'println' keyword
    | KeywordPrintln
    /// 'int' keyword
    | KeywordInt
    /// 'double' keyword
    | KeywordDouble
    /// 'string' keyword
    | KeywordString
    /// 'char' keyword
    | KeywordChar