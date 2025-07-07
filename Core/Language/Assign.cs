using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Assign(int row, int column, string variable, IExpression value) : ASTNode(row, column), IInstruction
{
    public string Variable { get; } = variable;
    public IExpression Value { get; } = value;

    public IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        context.Variables[Variable] = 0;
        return Value.CheckSemantic(context);
    }

    public void Evaluate(Context context) => context.Variables[Variable] = Value.Evaluate(context)!;

    public void SearchLabels(Context context) { }


}
