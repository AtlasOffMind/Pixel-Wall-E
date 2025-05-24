using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class UnitaryExpr<T>(int row, int column, IExpression<T> left) : Expressions<T>(row, column)
{
    public IExpression<T> Left { get; } = left;
    public abstract override T Evaluate(Context context);
}
