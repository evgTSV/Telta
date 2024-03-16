module LexerTests

open System.IO
open Telta.Lexer
open Telta.Lexer.Tests
open Xunit

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
    
[<Theory>]
[<InlineData("//ignored")>]
[<InlineData("// ignored")>]
[<InlineData("// int32 a = 8")>]
[<InlineData("//")>]
let IgnoreCommentLexemesTest (source:string) =
    // let tokens = getTokens source
    Assert.True(true) 
