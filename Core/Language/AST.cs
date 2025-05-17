using Lexer.Model;

namespace Core.Language;

public abstract class ASTNode(int row, int column)
{
    public Location Location { get; } = new Location(row, column);
}
