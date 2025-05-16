using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.AST.Expressions;

public class BinaryNumExpr(int row, int column, IExpression<int> left, IExpression<int> right, BinaryType type) : BinaryExpr<int>(row, column, left, right)
{
    public override int Evaluate(Context context) => type switch
    {
        BinaryType.SUM => Left.Evaluate(context) + Right.Evaluate(context),
        BinaryType.MINUS => Left.Evaluate(context) - Right.Evaluate(context),
        BinaryType.DIVISION => Left.Evaluate(context) / Right.Evaluate(context),
        BinaryType.MULTIPLICATION => Left.Evaluate(context) * Right.Evaluate(context),
        BinaryType.MODULE => Left.Evaluate(context) % Right.Evaluate(context),
        BinaryType.POW => (int)Math.Pow(left.Evaluate(context), Right.Evaluate(context)),

        _ => throw new NotImplementedException(),

    };
}
