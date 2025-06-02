using Core.Model;

namespace Lexer.Model;

public static class LocationFactory
{
    public static Location Create(int file, int startColumn, int endColumn)
    {
        return new Location(file, startColumn, endColumn);
    }

    public static Location Create(int file, int startColumn, string lex)
    {
        return new Location(file, startColumn, startColumn + lex.Length);
    }

    public static Location Create(Token token)
    {
        return new Location(token.row, token.column, token.column + token.name.Length);
    }
}
