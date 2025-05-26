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

        // TODO hacer otro recorrido para buscar los labels
        // ast.SearchLabels(context); (hacer interfaz ISerachLabel para hacer el recorrido de la busqueda de labels)
        //Search label sea solo para las instrucciones 
        // ISL busca en el bloque de instrucciones todas las instrucciones tipo label y evaluandolas antes que todas las demas
        // ast.CheckSemantic(context);
        ast.Evaluate(context);
    }
}