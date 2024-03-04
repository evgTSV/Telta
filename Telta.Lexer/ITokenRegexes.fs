namespace Telta.Lexer

[<Interface>]
type ITokenRegexes<'tokenType> =
    abstract FindMatchToken:string -> 'tokenType