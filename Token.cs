using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public enum TokenType
{
    ReservedWord,
    Keyword,
    Operator,
    Punctuation,
    Number,
    StringLiteral,
    CharLiteral,
    PreprocessorDirective,
    Comment,
    Identifier,
    Unknown
}

public class Token
{
    public string Lexeme { get; set; }
    public TokenType Type { get; set; }

    public Token(string lexeme, TokenType type)
    {
        Lexeme = lexeme;
        Type = type;
    }
}
