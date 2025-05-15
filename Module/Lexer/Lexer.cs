using System.Text;

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



    public class Scanner
    {
        public static List<Exception> exceptions = [];
        public static readonly Dictionary<string, TokenType> Dictionary = new()
            {
                //Aritmetic expressions
                { "+", TokenType.PLUS },
                { "-", TokenType.MINUS},
                { "/", TokenType.DIVISION},
                { "*", TokenType.MULTIPLICATION },
                { "=", TokenType.EQUAL},
                { "**", TokenType.POW},
                { "%", TokenType.MODULE},

                // Literals.
                {"<-",TokenType.ASSIGN},

                //Booleans Expressions
                { "==",TokenType.EQUAL_EQUAL},
                { ">",TokenType.GREATER},
                { ">=",TokenType.GREATER_EQUAL},
                { "<",TokenType.LESS},
                { "<=",TokenType.LESS_EQUAL},
                { "&&",TokenType.AND},
                { "||",TokenType.OR},
                { "&",TokenType.AND},
                { "|",TokenType.OR}, // mas presedencia que el AND
                { "false",TokenType.FALSE},
                { "true",TokenType.TRUE},

                //Keywords
                {"GOTO",TokenType.GOTO},
                {"\"RED\"", TokenType.COLOR},
                {"\"BLUE\"", TokenType.COLOR},
                {"\"GREEN\"", TokenType.COLOR},
                {"\"YELLOW\"", TokenType.COLOR},
                {"\"ORANGE\"", TokenType.COLOR},
                {"\"PURPLE\"", TokenType.COLOR},
                {"\"BLACK\"", TokenType.COLOR},
                {"\"WHITE\"", TokenType.COLOR},
                {"\"TRANSPARENT\"", TokenType.COLOR},

                { "(", TokenType.OPEN_PAREN},
                { ")", TokenType.CLOUSE_PAREN},
                { "[", TokenType.OPEN_PAREN},
                { "]", TokenType.CLOUSE_PAREN},


            };

        public static List<Token> Tokenizer(string[] line)
        {
            exceptions.Clear();
            StringBuilder current = new();
            List<Token> tokens = [];
            int columnPos = 0;
            bool reader = false;
   


            for (int i = 0; i < line.Length; i++)
            {
                Token? token;
                for (int j = 0; j < line[i].Length; j++)
                {
                    if (line[i][j] == '\"')
                        reader = !reader;

                    if (j + 1 < line[i].Length && Match(current + line[i][j + 1].ToString()))
                    {
                        current.Append(line[i][++j]);
                        continue;
                    }

                    if (reader || !IsSeparator(current, line[i][j]))
                    {
                        current.Append(line[i][j]);
                        continue;
                    }
                    if (SavingToken(current, i, columnPos, out token))
                        tokens.Add(token!);
                    columnPos = j + 1;
                }
                if (SavingToken(current, i, columnPos, out token))
                    tokens.Add(token!);

                tokens.Add(new Token(i, columnPos, TokenType.BACKSLASH, "\n"));
            }

            return tokens;
        }

        public static bool IsSeparator(StringBuilder current, char sep)
        {
            var temp = current.ToString() + sep;
            if ( Dictionary.ContainsKey(temp))
                return false;
            return sep switch
            {
                ' ' => true,
                '+' => true,
                '-' => true,
                '*' => true,
                '/' => true,
                '%' => true,
                ',' => true,
                '|' => true,
                '&' => true,
                '=' => true,
                '<' => true,
                '>' => true,
                ')' => true,
                '(' => true,
                '[' => true,
                ']' => true,
                _ => false,
            };
        }

        public static bool Match(string sep)
        {
            return sep switch
            {
                "**" => true,
                "||" => true,
                "&¬" => true,
                "==" => true,
                "<=" => true,
                ">=" => true,
                "<-" => true,
                _ => false,
            };
        }
        public static bool SavingToken(StringBuilder current, int i, int columnPos, out Token? token)
        {
            if (current.Length > 0)
            {
                bool istrue;
                token = null;
                string name = current.ToString();

                if (istrue = Dictionary.TryGetValue(name.ToUpper(), out TokenType type))
                {
                    token = new(i, columnPos, type, name);
                }
                else if (name[0] == '"')
                {
                    exceptions.Add(new InvalidOperationException(""));
                }
                else if (istrue = int.TryParse(name, out _))
                {
                    token = new(i, columnPos, TokenType.NUMBER, name);
                }
                else if (istrue = TryParseIdentifier(name))
                {
                    token = new Token(i, columnPos, TokenType.IDENTIFIER, $"{name}");
                }
                else exceptions.Add(new InvalidOperationException("mmmmmmmmmmmmmmmmmmmmmmm "));
                current.Clear();
                return istrue;
            }
            token = null;
            return false;
        }

        private static bool TryParseIdentifier(string name)
        {
            if (!char.IsLetter(name[0]))
                return false;
            for (int i = 1; i < name.Length - 1; i++)
            {
                if (!char.IsLetterOrDigit(name[i]) && name[0] != '-') return false;
            }
            return true;
        }
    }



}

