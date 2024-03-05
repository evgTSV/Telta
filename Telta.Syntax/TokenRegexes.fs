namespace Telta.Syntax

open System.Globalization
open Telta.Lexer
open System
open System.Text

type TokenRegexes() =
    interface ITokenRegexes<TokenType> with
        member this.FindMatchToken(lexeme:string) =
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
            
            | l when this.IsIdentifier l ->
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
            
            | l when this.IsStringLiteral l -> TokenType.StringLiteral
            | l when this.IsCharLiteral l -> TokenType.CharLiteral
            | l when this.IsIntNumber l -> TokenType.IntegerLiteral
            | l when this.IsRealNumber l -> TokenType.RealNumberLiteral
            
            | _ -> TokenType.Unknown
            
    member public this.FindMatchToken(lexeme:string) = (this :> ITokenRegexes<TokenType>).FindMatchToken(lexeme)
            
    member private this.IsIdentifier(lexeme:string) =
        (Char.IsLetter(lexeme[0]) || lexeme[0] = '_') && lexeme.Length < 100 && this.CheckIdentifier(lexeme)
        
    member private this.CheckIdentifier(id:string) =
        id
        |> Seq.where (fun(ch) -> not (Char.IsLetterOrDigit(ch)) && not (ch = '_'))
        |> Seq.isEmpty
    member private this.IsStringLiteral(literal:string) =
        literal.StartsWith("\"") && literal.EndsWith("\"")
        
    member private this.IsCharLiteral(literal:string) =
        literal.StartsWith("'") && literal.EndsWith("'")
    
    member private this.IsIntNumber(literal:string) =
        let isInt, _ = Int32.TryParse(literal)
        isInt
        
    member private this.IsRealNumber(literal:string) =
        let isReal, _ = Double.TryParse(literal, CultureInfo.InvariantCulture)
        isReal