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

    public void Evaluate(Context context) => context.Variables[Variable] = Value.Evaluate(context);
}

public abstract class Expressions<T>(int row, int column) : ASTNode(row, column), IExpression<T>
{
    public abstract T Evaluate(Context context);
}

public abstract class BinaryExpr<T>(int row, int column, IExpression<T> left, IExpression<T> right) : Expressions<T>(row, column)
{
    public IExpression<T> Left { get; } = left;
    public IExpression<T> Right { get; } = right;
    public abstract override T Evaluate(Context context);
}

public class BinaryNumExpr(
    int row, int column,
    IExpression<int> left,
    IExpression<int> right,
    BinaryType type) : BinaryExpr<int>(row, column, left, right)
{
    public override int Evaluate(Context context) => type switch
    {
        BinaryType.Sum => Left.Evaluate(context) + Right.Evaluate(context),
        _ => throw new NotImplementedException(),
    };
}
