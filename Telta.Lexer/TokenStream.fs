namespace Telta.Lexer

open System.Collections.Generic

type TokenStream<'tokenType>() =
    class
        let tokens = List<IToken<'tokenType>>()
        
        member public this.AddToken (token:IToken<'tokenType>) =
            tokens.Add(token)
    end