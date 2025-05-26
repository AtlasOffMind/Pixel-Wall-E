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
        var context = new Context([], []);

        var text = TextReader.TextRead(fileName);


        var arr = Scanner.Tokenizer(text.Split("\r\n"));

        Console.WriteLine(string.Join("\n", arr));

        var ast = parser.Parse(arr);

        // TODO por cada nodo revisar su checkeo, ver semanticamente cada nodo cuando da error
        //ejemplo: en assign declarar 2 veces la misma variable o si la declaran 2 veces con tipo distinto
        //       : cuando una suma no es valida, etc
        ast.SearchLabels(context);
        ast.CheckSemantic(context);
        ast.Evaluate(context);
    }
}