namespace Telta.Syntax

open System
open System.Globalization

module TokenRegexes =
    let private checkIdentifier(id:string) =
        id
        |> Seq.where (fun ch -> not (Char.IsLetterOrDigit(ch)) && not (ch = '_'))
        |> Seq.isEmpty
    let private isIdentifier (lexeme:string) =
        (Char.IsLetter(lexeme[0]) || lexeme[0] = '_') && lexeme.Length < 100 && checkIdentifier(lexeme)
        
    let private isStringLiteral(literal:string) =
        literal.StartsWith("\"") && literal.EndsWith("\"")
        
    let private isCharLiteral(literal:string) =
        literal.StartsWith("'") && literal.EndsWith("'") && literal.Length = 3
    
    let private isIntNumber(literal:string) =
        let isInt, _ = Int32.TryParse(literal)
        isInt
        
    let private isRealNumber(literal:string) =
        let isReal, _ = Double.TryParse(literal, CultureInfo.InvariantCulture)
        isReal

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
            
            | l when isIdentifier l ->
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
                    | "return" -> ReturnKeyword
                    | "use" -> UsingKeyword
                    | "namespace" -> NamespaceKeyword
                    | "class" -> ClassKeyword
                    | _ -> Identifier
            
            | l when isStringLiteral l -> StringLiteral
            | l when isCharLiteral l -> CharLiteral
            | l when isIntNumber l -> IntegerLiteral
            | l when isRealNumber l -> RealNumberLiteral
            
            | _ -> Unknown