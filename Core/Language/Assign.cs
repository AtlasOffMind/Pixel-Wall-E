using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Assign<T>(int row, int column, string variable, IExpression<T> value) : ASTNode(row, column), IInstruction
{
    public string Variable { get; } = variable;
    public IExpression<T> Value { get; } = value;

    public void Evaluate(Context context) => context.Variables[Variable] = Value.Evaluate(context)!;
}
