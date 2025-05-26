using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public class LexerService
{
    private readonly ICodeProvider _codeProvider;

    public LexerService(ICodeProvider codeProvider)
    {
        _codeProvider = codeProvider;
    }

    public List<Token> AnalyzeFromSource()
    {
        var code = _codeProvider.GetCode();
        return Lexer.Analyze(code);
    }
}

