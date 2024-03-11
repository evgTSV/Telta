namespace Telta.Lexer

type TokenStream() =
    member val tokens = ResizeArray<Token>()
    member public this.AddToken (token:Token) = this.tokens.Add(token)