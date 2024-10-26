namespace Telta.Syntax

type TokenType =
    // Specials
    /// Empty or whitespace
    | Empty
    /// New line token ('\r' or '\n' or '\r\n')
    | NewLine
    /// End of source
    | End
    /// Unrecognized
    | Unknown
    /// Identifier
    | Identifier
    
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
    /// '>='
    | GreaterOrEqual
    /// '<='
    | LessOrEqual
    
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
    /// '$'
    | DollarSign
    /// '//'
    | DoubleSlash
    /// '?'
    | QuestionMark
    /// '??'
    | DoubleQuestionMark
    
    
    // Literals
    /// string literal
    | StringLiteral
    /// Char literal
    | CharLiteral
    /// int32 literal
    | IntegerLiteral
    /// float literal
    | RealNumberLiteral
    // bool literal
    | BooleanLiteral
    
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
    /// 'bool' keyword
    | KeywordBool
    /// 'void' keyword
    | KeywordVoid
    /// 'return' keyword
    | ReturnKeyword
    /// 'use' keyword
    | UsingKeyword
    /// 'namespace' keyword
    | NamespaceKeyword
    /// 'class' keyword
    | ClassKeyword
    /// 'public' keyword
    | PublicKeyword
    /// 'private' keyword
    | PrivateKeyword
    /// 'literal' keyword
    | LiteralKeyword
    /// 'try' keyword
    | TryKeyword
    /// 'with' keyword
    | WithKeyword