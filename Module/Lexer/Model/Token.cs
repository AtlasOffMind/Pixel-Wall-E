using Core.Enum;

namespace Lexer.Model;

public class Token
{
    public int row { get; private set;}
    public int column { get; private set;}
    public TokenType Type { get; private set;}
    public string name { get; private set;}

    public Token(int row, int column, TokenType type, string name)
    {
        this.row = row;
        this.column = column;
        this.Type = type;
        this.name = name;
    }
}