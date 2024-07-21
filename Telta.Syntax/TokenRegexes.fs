namespace Telta.Syntax

open System
open System.Globalization
open FParsec

module TokenRegexes =
    
    [<Literal>]
    let IdentifierLengthLimit = 100
    
    let private identifier (lexeme:string) =
        if lexeme.Length > IdentifierLengthLimit then false
        else
        let idParser : Parser<string, unit> = many1Chars2 (asciiLetter <|> pchar '_') (asciiLetter <|> digit <|> pchar '_')
        match run idParser lexeme with
            | Success(_, _, position)
                when position.Index = lexeme.Length -> true
            | _ -> false
        
    let private stringLiteral(literal:string) =
        literal.StartsWith("\"") && literal.EndsWith("\"")
        
    let private charLiteral(literal:string) =
        literal.StartsWith("'") && literal.EndsWith("'") && literal.Length = 3
    
    let private intNumber(literal:string) =
        let isInt, _ = Int32.TryParse(literal)
        isInt
        
    let private realNumber(literal:string) =
        let isReal, _ = Double.TryParse(literal, CultureInfo.InvariantCulture)
        isReal
        
    let private booleanLiteral(literal:string) = literal = "true" || literal = "false"

    let findMatchToken(lexeme:string) =
        match lexeme with
            | l when l = Environment.NewLine -> NewLine
            | l when (String.IsNullOrWhiteSpace l) -> Empty
                   
            | "(" -> OpenParen
            | ")" -> CloseParen
            | "[" -> OpenBracket
            | "]" -> CloseBracket
            | "{" -> OpenBrace
            | "}" -> CloseBrace
            | "<" -> OpenAngleBracket
            | ">" -> CloseAngleBracket
            
            | "==" -> Equal
            | "!=" -> NotEqual
            | "&&" -> LogicalAnd
            | "||" -> LogicalOr
            | "!" -> LogicalNot
            | ">=" -> GreaterOrEqual
            | "<=" -> LessOrEqual
            
            | "+|" -> IncrementPrefix
            | "|+" -> IncrementPostfix
            | "-|" -> DecrementPrefix
            | "|-" -> DecrementPostfix
            
            | "+" -> Add
            | "+=" -> AddAssign
            | "-" -> Subtract
            | "-=" -> SubtractAssign
            | "*" -> Multiple
            | "*=" -> MultipleAssign
            | "/" -> Divide
            | "/=" -> DivideAssign
            | "%" -> Module
            | "%=" -> ModuleAssign
            | "**" -> Pow
            | "**=" -> PowAssign
            
            | "&" -> BitAnd
            | "&=" -> BitAndAssign
            | "|" -> BitOr
            | "|=" -> BitOrAssign
            | "^" -> BitXor
            | "^=" -> BitXorAssign
            | "~" -> BitNot
            | "<<" -> ShiftLeft
            | "<<=" -> ShiftLeftAssign
            | ">>" -> ShiftRight
            | ">>=" -> ShiftRightAssign
            
            | "->" -> TokenType.Array
            | "." -> Dot
            | "," -> Comma
            | ":" -> Colon
            | ";" -> Semicolon
            | "=" -> Assign
            | "`" -> Backquote
            | "'" -> QuotationMark
            | "\"" -> DoubleQuotationMark
            | "$" -> DollarSign
            | "//" -> DoubleSlash
            | "?" -> QuestionMark
            | "??" -> DoubleQuestionMark
            
            | l when stringLiteral l -> StringLiteral
            | l when charLiteral l -> CharLiteral
            | l when intNumber l -> IntegerLiteral
            | l when realNumber l -> RealNumberLiteral
            | l when booleanLiteral l -> BooleanLiteral
            
            | l when identifier l ->
                match l with
                    | "if" -> KeywordIf
                    | "elif" -> KeywordElif
                    | "else" -> KeywordElse
                    | "goto" -> KeywordGoto
                    | "print" -> KeywordPrint
                    | "println" -> KeywordPrintln
                    | "string" -> KeywordString
                    | "char" -> KeywordChar
                    | "int32" -> KeywordInt
                    | "double" -> KeywordDouble
                    | "bool" -> KeywordBool
                    | "void" -> KeywordVoid
                    | "return" -> ReturnKeyword
                    | "use" -> UsingKeyword
                    | "namespace" -> NamespaceKeyword
                    | "class" -> ClassKeyword
                    | "public" -> PublicKeyword
                    | "private" -> PrivateKeyword
                    | "literal" -> LiteralKeyword
                    | _ -> Identifier
            
            | _ -> Unknown