using System;

namespace Lab1;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter file name:");
        var filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        ICodeProvider provider = new CodeProvider(filename);
        var service = new LexerService(provider);
        var tokens = service.AnalyzeFromSource();

        foreach (var token in tokens)
        {
            Console.WriteLine($"<{token.Lexeme} , {token.Type}>");
        }
    }
}
