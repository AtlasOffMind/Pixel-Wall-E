using TextReader = Lexer.TextReader;
using Lexer;

namespace Test;

public class Program
{
    public static void Main(string[] args)
    {
        var fileName = "Test File.txt";

        var text = TextReader.TextRead(fileName);

        System.Console.WriteLine(string.Join("|| ", text.Split("\r\n")));

        //        Tokenizer.Tokening(text.Split('\n'));


    }
}