using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class UniBoolExpr(int row, int column, IExpression<bool> left) : UnitaryExpr<bool>(row, column, left)
{
    public override bool Evaluate(Context context) => !Left.Evaluate(context);
}
