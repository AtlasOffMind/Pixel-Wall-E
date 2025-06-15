using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.Expressions;

public class BinaryNumExpr(int row, int column, IExpression left, IExpression right, BinaryType type) : BinaryExpr<int>(row, column, left, right)
{
    public override object Evaluate(Context context) => type switch
    {
        BinaryType.SUM => (int)Left.Evaluate(context) + (int)Right.Evaluate(context),
        BinaryType.MINUS => (int)Left.Evaluate(context) - (int)Right.Evaluate(context),
        BinaryType.DIVISION => (int)Left.Evaluate(context) / (int)Right.Evaluate(context),
        BinaryType.MULTIPLICATION => (int)Left.Evaluate(context) * (int)Right.Evaluate(context),
        BinaryType.MODULE => (int)Left.Evaluate(context) % (int)Right.Evaluate(context),
        BinaryType.POW => (int)Math.Pow((int)Left.Evaluate(context), (int)Right.Evaluate(context)),

        _ => throw new NotImplementedException(),

    };
}
