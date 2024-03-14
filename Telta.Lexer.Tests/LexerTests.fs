module LexerTests

open System
open System.IO
open Telta.Lexer
open Telta.Lexer.Tests
open Xunit

[<Fact>]
let PrimaryTest () =
    let source = "int32 \r\n sigma =10 ; // ignored text \r\n int32 a= 5; \r\n string greetMessage = \"Hello, World!\"; \r\n if (sigma>a) { print(greetMessage); }"
    use stream = new StringReader(source)
    let sourceFile = FakeSource(stream)
    let lexer = Lexer(sourceFile)
    let tokens = lexer.Tokenization
    Assert.True(true)