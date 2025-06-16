using TextReader = Lexer.TextReader;
using Lexer;
using Core.Model;

namespace Test;

public class Program
{
    public static void Main(string[] args)
    {
        var fileName = "Test File.txt";
        var parser = new Parser.Parser();

        // var context = new Context();

        var text = TextReader.TextRead(fileName);


        var arr = Scanner.Tokenizer(text.Split("\r\n"));

        Console.WriteLine(string.Join("\n", arr));

        var ast = parser.Parse(arr);

        // ast.SearchLabels(context);
        // ast.CheckSemantic(context);
        // ast.Evaluate(context);
    }
}