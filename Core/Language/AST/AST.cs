using Core.Interface;
using Core.Model;
using Lexer.Model;

namespace Core.Language.AST;

public abstract class ASTNode(int row, int column)
{
    public Location Location { get; } = new Location(row, column);
}

public class Label(int row, int column, string name) : ASTNode(row, column), ISemantic
{
    public string Name { get; } = name;

    public bool CheckSemantic(Context context)
    {
        if (context.Labels.ContainsKey(Name))
            return false;
        context.Labels[Name] = Location.Row;
        return true;
    }
}

public class Assign<T>(int row, int column, string variable, IExpression<T> value) : ASTNode(row, column), IInstruction
{
    public string Variable { get; } = variable;
    public IExpression<T> Value { get; } = value;

    public void Evaluate(Context context) => context.Variables[Variable] = Value.Evaluate(context)!;
}
