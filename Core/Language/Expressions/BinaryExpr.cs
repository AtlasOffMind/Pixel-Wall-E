using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class BinaryExpr<T, K>(int row, int column, IExpression left, IExpression right)
    : ASTNode(row, column), IExpression
{
    public IExpression Left { get; } = left;
    public IExpression Right { get; } = right;
    public abstract object Evaluate(Context context);
    public IEnumerable<SemanticError> CheckSemantic(Context context)
        => Left.CheckSemantic(context).Concat(Right.CheckSemantic(context));
}

public abstract class BinaryExpr<T>(int row, int column, IExpression left, IExpression right) : BinaryExpr<T, T>(row, column, left, right);
