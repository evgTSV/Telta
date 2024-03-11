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
        | l when l = Environment.NewLine -> TokenType.NewLine
            | l when (String.IsNullOrWhiteSpace l) -> TokenType.Empty
                   
            | "(" -> TokenType.OpenParen
            | ")" -> TokenType.CloseParen
            | "[" -> TokenType.OpenBracket
            | "]" -> TokenType.CloseBracket
            | "{" -> TokenType.OpenBrace
            | "}" -> TokenType.CloseBrace
            | "<" -> TokenType.OpenAngleBracket
            | ">" -> TokenType.CloseAngleBracket
            
            | "==" -> TokenType.Equal
            | "!=" -> TokenType.NotEqual
            | "&&" -> TokenType.LogicalAnd
            | "||" -> TokenType.LogicalOr
            | "!" -> TokenType.LogicalNot
            
            | "+|" -> TokenType.IncrementPrefix
            | "|+" -> TokenType.IncrementPostfix
            | "-|" -> TokenType.DecrementPrefix
            | "|-" -> TokenType.DecrementPostfix
            
            | "+" -> TokenType.Add
            | "+=" -> TokenType.AddAssign
            | "-" -> TokenType.Subtract
            | "-=" -> TokenType.SubtractAssign
            | "*" -> TokenType.Multiple
            | "*=" -> TokenType.MultipleAssign
            | "/" -> TokenType.Divide
            | "/=" -> TokenType.DivideAssign
            | "%" -> TokenType.Module
            | "%=" -> TokenType.ModuleAssign
            | "**" -> TokenType.Pow
            | "**=" -> TokenType.PowAssign
            
            | "&" -> TokenType.BitAnd
            | "&=" -> TokenType.BitAndAssign
            | "|" -> TokenType.BitOr
            | "|=" -> TokenType.BitOrAssign
            | "^" -> TokenType.BitXor
            | "^=" -> TokenType.BitXorAssign
            | "~" -> TokenType.BitNot
            | "<<" -> TokenType.ShiftLeft
            | "<<=" -> TokenType.ShiftLeftAssign
            | ">>" -> TokenType.ShiftRight
            | ">>=" -> TokenType.ShiftRightAssign
            
            | "->" -> TokenType.Array
            | "." -> TokenType.Dot
            | "," -> TokenType.Comma
            | ":" -> TokenType.Colon
            | ";" -> TokenType.Semicolon
            | "=" -> TokenType.Assign
            | "`" -> TokenType.Backquote
            | "'" -> TokenType.QuotationMark
            | "\"" -> TokenType.DoubleQuotationMark
            | "$" -> TokenType.DollarSign
            | "//" -> TokenType.DoubleSlash
            
            | l when isIdentifier l ->
                match l with
                    | "if" -> TokenType.KeywordIf
                    | "elif" -> TokenType.KeywordElif
                    | "else" -> TokenType.KeywordElse
                    | "goto" -> TokenType.KeywordGoto
                    | "print" -> TokenType.KeywordPrint
                    | "println" -> TokenType.KeywordPrintln
                    | "string" -> TokenType.KeywordString
                    | "char" -> TokenType.KeywordChar
                    | "int32" -> TokenType.KeywordInt
                    | "double" -> TokenType.KeywordDouble
                    | _ -> TokenType.Identifier
            
            | l when isStringLiteral l -> TokenType.StringLiteral
            | l when isCharLiteral l -> TokenType.CharLiteral
            | l when isIntNumber l -> TokenType.IntegerLiteral
            | l when isRealNumber l -> TokenType.RealNumberLiteral
            
            | _ -> TokenType.Unknown