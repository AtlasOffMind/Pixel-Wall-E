using Core.Language.Expressions;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Goto(int row, int column, string labelName, IExpression<bool> cond) : ASTNode(row, column), IInstruction
{
    public string LabelName { get; } = labelName;
    public IExpression<bool> Cond { get; } = cond;

    public bool CheckSemantic(Context context)
    {
        return context.Labels.ContainsKey(LabelName)
            && Cond.CheckSemantic(context);
    }

    public void Evaluate(Context context)
    {
        context.JumpCond = Cond.Evaluate(context);
        context.JumpTo = LabelName;
    }

    public void SearchLabels(Context context) { }
}