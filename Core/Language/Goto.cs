using Core.Language.Expressions;
using Core.Interface;
using Core.Model;
using Core.Error;

namespace Core.Language;

public class Goto(int row, int column, string labelName, IExpression cond) : ASTNode(row, column), IInstruction
{
    public string LabelName { get; } = labelName;
    public IExpression Cond { get; } = cond;

    public IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        if (!context.Labels.ContainsKey(LabelName))
            yield return new SemanticError(Location, "");
        foreach (var item in Cond.CheckSemantic(context))
            yield return item;
    }

    public void Evaluate(Context context)
    {
        context.JumpCond = (bool)Cond.Evaluate(context);
        context.JumpTo = LabelName;
    }

    public void SearchLabels(Context context) { }
}