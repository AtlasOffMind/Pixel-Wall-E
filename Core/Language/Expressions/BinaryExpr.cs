using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class BinaryExpr<T, K>(int row, int column, IExpression<K> left, IExpression<K> right) : Expressions<T>(row, column)
{
    public IExpression<K> Left { get; } = left;
    public IExpression<K> Right { get; } = right;
    public abstract override T Evaluate(Context context);
}

public abstract class BinaryExpr<T>(int row, int column, IExpression<T> left, IExpression<T> right) : BinaryExpr<T, T>(row, column, left, right);
