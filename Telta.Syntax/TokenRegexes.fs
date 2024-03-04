namespace Telta.Syntax

open Telta.Lexer
open System

type TokenRegexes =
    interface ITokenRegexes<TokenType> with
        member this.FindMatchToken(lexeme:string) =
            match lexeme with
            | l when String.IsNullOrWhiteSpace l -> TokenType.Empty
            | l when l = Environment.NewLine -> TokenType.NewLine
            
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
            
            | l when this.IsStringLiteral l -> TokenType.StringLiteral
            | l when this.IsCharLiteral l -> TokenType.CharLiteral
            | l when this.IsIntNumber l -> TokenType.IntegerLiteral
            | l when this.IsRealNumber l -> TokenType.RealNumberLiteral
            
            | _ -> TokenType.Unknown
    
    member private this.IsStringLiteral(literal:string) =
        literal.StartsWith("\"") && literal.EndsWith("\"")
        
    member private this.IsCharLiteral(literal:string) =
        literal.StartsWith("'") && literal.EndsWith("'")
    
    member private this.IsIntNumber(literal:string) =
        let (isInt, _) = Int32.TryParse(literal)
        isInt
        
    member private this.IsRealNumber(literal:string) =
        let (isReal, _) = Double.TryParse(literal)
        isReal