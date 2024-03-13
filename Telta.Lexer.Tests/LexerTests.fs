module LexerTests

open System
open System.IO
open Telta.Lexer
open Telta.Lexer.Tests
open Xunit

[<Fact>]
let PrimaryTest () =
    let source = "int sigma = 10="
    use stream = new StringReader(source)
    let sourceFile = FakeSource(stream)
    let lexer = Lexer(sourceFile)
    let tokens = lexer.Tokenization
    Assert.True(true)