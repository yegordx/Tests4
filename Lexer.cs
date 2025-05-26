using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1;

public class Lexer
{
    private readonly List<string> Reserved = new() {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
        "checked", "class", "const", "continue", "decimal", "default", "delegate",
        "do", "double", "else", "enum", "event", "explicit", "extern", "false",
        "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit",
        "in", "int", "interface", "DateTime", "internal", "is", "lock", "long", "namespace",
        "new", "null", "object", "operator", "out", "override", "params", "private", "partial",
        "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
        "short", "sizeof", "stackalloc", "static", "string", "struct", "switch",
        "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked",
        "unsafe", "ushort", "using", "var", "virtual", "void", "volatile", "while", "get", "set", "Guid"
    };
    private readonly List<string> Operators = new() {
        "=", "+", "-", "*", "/", "%", "&&", "||", "!", "==", "!=", "<", ">", "<=", ">=",
        "+=", "-=", "*=", "/=", "%=", "&&=", "||=", "++", "--"
    };
    private readonly List<string> Punctuation = new() {
        "(", ")", "{", "}", "[", "]", ",", ";", ":", "=>", "."
    };
    private readonly HashSet<string> Identifiers = new();

    public List<Token> Analyze(string code)
    {
        var tokens = new List<Token>();
        var pattern = @"(//.*?$|/\*.*?\*/|""(\\.|[^\\\""])*""|'[^']*'|#[^\n]*|\b\w+\b|\S)";
        var matches = Regex.Matches(code, pattern, RegexOptions.Singleline | RegexOptions.Multiline);

        foreach (Match match in matches)
        {
            string word = match.Value;
            TokenType type = TokenType.Unknown;

            if (word.StartsWith("//") || word.StartsWith("/*"))
                type = TokenType.Comment;
            else if (word.StartsWith("#"))
                type = TokenType.PreprocessorDirective;
            else if (word.StartsWith("\"") && word.EndsWith("\""))
                type = TokenType.StringLiteral;
            else if (word.StartsWith("'") && word.EndsWith("'") && word.Length == 3)
                type = TokenType.CharLiteral;
            else if (Reserved.Contains(word))
                type = TokenType.Keyword;
            else if (Operators.Contains(word))
                type = TokenType.Operator;
            else if (Punctuation.Contains(word))
                type = TokenType.Punctuation;
            else if (double.TryParse(word, out _))
                type = TokenType.Number;
            else if (IsValidIdentifier(word))
                type = TokenType.Identifier;

            tokens.Add(new Token(word, type));
        }

        return tokens;
    }

    public bool IsValidIdentifier(string identifier)
    {
        if (Identifiers.Contains(identifier)) return true;
        if (identifier.StartsWith("@")) identifier = identifier[1..];
        if (!char.IsLetter(identifier[0]) && identifier[0] != '_') return false;
        if (!identifier.All(c => char.IsLetterOrDigit(c) || c == '_')) return false;
        Identifiers.Add(identifier);
        return true;
    }
}
