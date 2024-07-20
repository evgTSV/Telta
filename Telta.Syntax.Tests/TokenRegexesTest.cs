namespace Telta.Syntax.Tests;

public class TokenRegexesTest
{
    [Fact]
    public void NewLineDetectionTest()
    {
        string lexeme = Environment.NewLine;
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.NewLine, tokenType);
    }

    [Theory]
    [InlineData("\"abc\"")]
    [InlineData("\"\"")]
    [InlineData("\"123\"")]
    public void StringLiteralDetectionTest(string lexeme)
    {
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.StringLiteral, tokenType);
    }
    
    [Theory]
    [InlineData("'A'")]
    [InlineData("' '")]
    [InlineData("'1'")]
    [InlineData("'\n'")]
    public void CharLiteralDetectionTest(string lexeme)
    {
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.CharLiteral, tokenType);
    }
    
    [Theory]
    [InlineData("001")]
    [InlineData("0")]
    [InlineData("123")]
    public void IntLiteralDetectionTest(string lexeme)
    {
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.IntegerLiteral, tokenType);
    }
    
    [Theory]
    [InlineData("001.34")]
    [InlineData("0,33")]
    [InlineData("10.")]
    [InlineData("10.0")]
    public void RealNumLiteralDetectionTest(string lexeme)
    {
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.RealNumberLiteral, tokenType);
    }
    
    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    public void BooleanLiteralDetectionTest(string lexeme)
    {
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.BooleanLiteral, tokenType);
    }

    [Theory]
    [InlineData("")]
    [InlineData($"empty")]
    [InlineData($" ")]
    [InlineData($"\u2000")]
    [InlineData(null)]
    public void EmptyDetectionTest(string lexeme)
    {
        if (lexeme == "empty") lexeme = String.Empty;

        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.Empty, tokenType);
    }

    [Theory]
    [InlineData("1name")]
    [InlineData("%value")]
    [InlineData("length_over_limit")]
    [InlineData("int_var%able3")]
    public void InvalidCustomIdentifierTest(string lexeme)
    {
        if (lexeme == "length_over_limit") lexeme = new string('a', TokenRegexes.IdentifierLengthLimit + 1);
        
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.NotEqual(TokenType.Identifier, tokenType);
    }
    
    [Theory]
    [InlineData("_name")]
    [InlineData("value")]
    [InlineData("int_variable3")]
    [InlineData("Vector2")]
    public void ValidCustomIdentifierTest(string lexeme)
    {
        var tokenType = TokenRegexes.findMatchToken(lexeme);
        
        Assert.Equal(TokenType.Identifier, tokenType);
    }
}