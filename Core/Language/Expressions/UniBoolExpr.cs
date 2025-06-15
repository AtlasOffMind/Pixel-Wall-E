using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class UniBoolExpr(int row, int column, IExpression left) : UnitaryExpr<bool>(row, column, left)
{
    public override object Evaluate(Context context) => !(bool)Left.Evaluate(context);
}


public class UniNumExpr(int row, int column, IExpression left) : UnitaryExpr<int>(row, column, left)
{
    public override object Evaluate(Context context) => -(int)Left.Evaluate(context);
}