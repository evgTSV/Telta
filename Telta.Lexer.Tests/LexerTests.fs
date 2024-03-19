module LexerTests

open System.Collections.Generic
open System.IO
open Telta.Lexer
open Telta.Lexer.Tests
open Telta.Syntax
open Xunit

let private testTokenization (source:string) testFunc =
    use stream = new StringReader(source)
    let sourceFile = FakeSource(stream)
    let lexer = Lexer(sourceFile)
    let tokens = lexer.Tokenization
    testFunc tokens

[<Fact>]
let EmptySourceTest () =
    let source = ""
    testTokenization source (fun tokens ->
        (tokens :> IEnumerable<Token>) |> Assert.Single |> ignore
        Assert.Equal(tokens.Read.TokenType, End)
        )
    
let AssignNewVariablesTest (tokens:TokenStream) (definedType:TokenType) (value:TokenType) =
    Assert.Equal(6, tokens.Length)
    let expectedTokens = [|
        definedType; TokenType.Identifier; TokenType.Assign; value; TokenType.Semicolon; TokenType.End
    |]
    Assert.Equivalent(expectedTokens, (tokens :> IEnumerable<Token>) |> Seq.map (_.TokenType))
    
[<Theory>]
[<InlineData("int32 num = 10;")>]
[<InlineData("int32 NUM= 10 ;")>]
[<InlineData("int32 NUM_= 10;")>]
[<InlineData("int32 _num=10;")>]
[<InlineData("int32 my_num =  10 ; // myNum")>]
let IntegerLiteralAssignNewVariablesTest (source:string) =
    testTokenization source (fun tokens ->
        AssignNewVariablesTest tokens TokenType.KeywordInt TokenType.IntegerLiteral)
    
[<Theory>]
[<InlineData("double num = 10.;")>]
[<InlineData("double NUM= 10.3 ;")>]
[<InlineData("double NUM_= 10.0;")>]
[<InlineData("double _num=1,0;")>]
[<InlineData("double my_num =  0,10 ; // myNum")>]
let RealNumberLiteralAssignNewVariablesTest (source:string) =
    testTokenization source (fun tokens ->
        AssignNewVariablesTest tokens TokenType.KeywordDouble TokenType.RealNumberLiteral)
    
[<Theory>]
[<InlineData("string text = \"Hello, World!\";")>]
[<InlineData("string TEXT= \"Hello, World!\" ;")>]
[<InlineData("string TEXT_= \"Hello, World!\";")>]
[<InlineData("string _text=\"Hello, World!\";")>]
[<InlineData("string my_text =  \"Hello, World!\" ; // myText")>]
let StringLiteralAssignNewVariablesTest (source:string) =
    testTokenization source (fun tokens ->
        AssignNewVariablesTest tokens TokenType.KeywordString TokenType.StringLiteral)
    
[<Theory>]
[<InlineData("char symbol = '\n';")>]
[<InlineData("char SYMBOL= '\n' ;")>]
[<InlineData("char SYMBOL_= '\n';")>]
[<InlineData("char _symbol='\n';")>]
[<InlineData("char my_text =  '\n' ; // myChar")>]
let CharLiteralAssignNewVariablesTest (source:string) =
    testTokenization source (fun tokens ->
        AssignNewVariablesTest tokens TokenType.KeywordChar TokenType.CharLiteral)
    
[<Theory>]
[<InlineData("Point new = old;")>]
[<InlineData("Point NEW= old ;")>]
[<InlineData("Point new_= old;")>]
[<InlineData("Point _new=old;")>]
[<InlineData("Point my_new =  old ; // myNewPoint")>]
let CustomTypeAssignVariablesTest (source:string) =
    testTokenization source (fun tokens ->
        AssignNewVariablesTest tokens TokenType.Identifier TokenType.Identifier)
    
[<Theory>]
[<InlineData("//ignored")>]
[<InlineData("// ignored")>]
[<InlineData("//")>]
let IgnoreCommentLexemesTest (source:string) =
    testTokenization source Assert.Single

[<Fact>] 
let MultilineStatementTest() =
    let source = """if (a == "hello!")
{
    a = "Goodbye";
}"""
    let expectedTokens = [|
        KeywordIf; OpenParen; Identifier; Equal; StringLiteral; CloseParen; NewLine
        OpenBrace; NewLine
        Identifier; Assign; StringLiteral; Semicolon; NewLine
        CloseBrace; End;
    |]
    testTokenization source (fun tokens ->
        Assert.Equal(expectedTokens.Length, tokens.Length)
        Assert.Equivalent(expectedTokens, (tokens :> IEnumerable<Token>) |> Seq.map (_.TokenType))
        )