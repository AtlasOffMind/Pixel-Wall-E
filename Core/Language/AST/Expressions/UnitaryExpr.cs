using Core.Interface;
using Core.Model;

namespace Core.Language.AST.Expressions;

public abstract class UnitaryExpr<T>(int row, int column, IExpression<T> left, IExpression<T> right) : Expressions<T>(row, column)
{
    public IExpression<T> Left { get; } = left;
    public IExpression<T> Right { get; } = right;
    public abstract override T Evaluate(Context context);
}
public abstract class UniBoolExpr<T>(int row, int column, IExpression<char> left, IExpression<bool> right) : UnitaryExpr<T>(row, column, left, right)
{

}
