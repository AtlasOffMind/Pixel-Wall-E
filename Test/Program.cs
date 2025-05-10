using TextReader = Lexer.TextReader;
using Lexer;

namespace Test;

public class Program
{
    public static void Main(string[] args)
    {
        var fileName = "Test File.txt";

        var text = TextReader.TextRead(fileName);


        var arr = Scanner.Tokenizer(text.Split("\r\n"));

        Console.WriteLine(string.Join("\n", arr));

    }
}