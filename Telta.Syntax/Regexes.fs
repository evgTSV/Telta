namespace Telta.Syntax

module Regexes =
    
    /// Telta identifier
    let Identifier = "[A-Za-z_][A-Za-z0-9_]*"
    
    // Literals
    /// Single-line string
    let StringLiteral = @"""((\\[^\n\r])|[^\r\n\\""])*"""
    /// Unicode char
    let CharLiteral = ""
    /// Int32 number
    let IntegerLiteral = "[0-9]+"
    /// Floating point number
    let RealNumberLiteral = ""