using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class BinaryExpr<T, K>(int row, int column, IExpression<K> left, IExpression<K> right)
    : ASTNode(row, column), IExpression<T>
{
    public IExpression<K> Left { get; } = left;
    public IExpression<K> Right { get; } = right;
    public abstract T Evaluate(Context context);
    public bool CheckSemantic(Context context)
        => Left.CheckSemantic(context) && Right.CheckSemantic(context);
}

public abstract class BinaryExpr<T>(int row, int column, IExpression<T> left, IExpression<T> right) : BinaryExpr<T, T>(row, column, left, right);
