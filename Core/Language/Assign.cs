using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Assign(int row, int column, string variable, IExpression<object> value) : ASTNode(row, column), IInstruction
{
    public string Variable { get; } = variable;
    public IExpression<object> Value { get; } = value;

    public bool CheckSemantic(Context context) => true;

    public void Evaluate(Context context) => context.Variables[Variable] = Value.Evaluate(context)!;
}
