using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.AST.Expressions;

public abstract class BinaryExpr<T>(int row, int column, IExpression<T> left, IExpression<T> right) : Expressions<T>(row, column)
{
    public IExpression<T> Left { get; } = left;
    public IExpression<T> Right { get; } = right;
    public abstract override T Evaluate(Context context);
}

public class BinaryNumExpr(
    int row, int column,
    IExpression<int> left,
    IExpression<int> right,
    BinaryType type) : BinaryExpr<int>(row, column, left, right)
{
    public override int Evaluate(Context context) => type switch
    {
        BinaryType.SUM => Left.Evaluate(context) + Right.Evaluate(context),
        _ => throw new NotImplementedException(),
        BinaryType.MINUS => Left.Evaluate(context) - Right.Evaluate(context),
        _ => throw new NotImplementedException(),
        BinaryType.DIVISION => Left.Evaluate(context) / Right.Evaluate(context),
        _ => throw new NotImplementedException(),
        BinaryType.MULTIPLICATION => Math.Pow(Left.Evaluate(context), Left.Evaluate(context)),
        _ => throw new NotImplementedException(),
        BinaryType.MODULE => Left.Evaluate(context) % Right.Evaluate(context),
        _ => throw new NotImplementedException(),

    };
}