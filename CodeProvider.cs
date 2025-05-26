using System.IO;

namespace Lab1;

public class CodeProvider : ICodeProvider
{
    private readonly string _path;

    public CodeProvider(string path)
    {
        _path = path;
    }

    public string GetCode()
    {
        return File.ReadAllText(_path);
    }
}

