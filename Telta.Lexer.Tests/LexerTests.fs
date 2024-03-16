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
let PrimaryTest () =
    let mutable source = """int32 sigma =10 ; // ignored text
int32 a= 5;
string greetMessage = "Hello, World!";
if (sigma>a)
{
    print(greetMessage);
    
    if (a >=2) {
            a |+;
            +| sigma;
    }
}

decimal realNum = 4,5;
decimal realNum2 = 4.5;
decimal realNum3 = 4.;

realNum3 += (decimal)sigma;//"""
    use stream = new StringReader(source)
    let sourceFile = FakeSource(stream)
    let lexer = Lexer(sourceFile)
    let tokens = lexer.Tokenization
    Assert.True(true)
    
let AssignNewVariablesTest (tokens:TokenStream) (definedType:TokenType) (value:TokenType) =
    Assert.Equal(6, tokens.Length)
    let expectedTokens = [|
        definedType; TokenType.Identifier; TokenType.Assign; value; TokenType.Semicolon; TokenType.End
    |]
    Assert.Equivalent(expectedTokens, (tokens :> IEnumerable<Token>) |> Seq.map (_.Type))
    
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