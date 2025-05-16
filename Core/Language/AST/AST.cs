using Lexer.Model;

namespace Core.Language.AST;

public abstract class ASTNode(int row, int column)
{
    public Location Location { get; } = new Location(row, column);
}
