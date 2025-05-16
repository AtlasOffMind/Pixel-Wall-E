using Core.Interface;
using Core.Model;

namespace Core.Language.AST.Expressions;

public abstract class UnitaryExpr<T>(int row, int column, IExpression<T> left) : Expressions<T>(row, column)
{
    public IExpression<T> Left { get; } = left;
    public abstract override T Evaluate(Context context);
}
public class UniBoolExpr(int row, int column, IExpression<bool> left) : UnitaryExpr<bool>(row, column, left)
{
    public override bool Evaluate(Context context)
    {
            return !left.Evaluate(context);
    }
}
