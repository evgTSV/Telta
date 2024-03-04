namespace Telta.Lexer

[<Interface>]
type IToken<'tokenType> =
    abstract member Type : 'tokenType
        with get
    abstract member Location : Location
    abstract member Text : string
        with get