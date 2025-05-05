using Core.Enum;
using Lexer.Model;

namespace Lexer
{
    public static class TextReader
    {
        public static string? text;


        public static string TextRead(string fileName)
        {
            fileName = Path.Combine(@"C:\My things\Git\2nd Proyect Pixel Wall-E\Content", fileName);

            var file = File.ReadAllBytes(fileName);
            var stream = new MemoryStream(file);

            // Open the text file using a stream reader.
            StreamReader reader = new(stream);

            // Read the stream as a string.
            return text = reader.ReadToEnd();
        }

    }



    public class Tokenizer
    {
        public static readonly Dictionary<char, TokenType> Dictionary = new()
            {
                { '+', TokenType.Plus },
                { '-', TokenType.Minus },
                { '/', TokenType.Division },
                { '*', TokenType.Multiplication},
                { '=', TokenType.Equal },
                { '(', TokenType.Open_Paren},
                { ')', TokenType.Clouse_Paren },
            };


        public static List<Token> Tokening(string[] line)
        {
            List<Token> tokens = [];

            for (int i = 0; i < line.Length - 1; i++)
            {
                for (int j = 0; j < line[i].Length; j++)
                {
                    if (line[i][j] == ' ') continue;
                }
            }

            return tokens;
        }
    }
}

