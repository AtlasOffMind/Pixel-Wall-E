using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.Expressions;

public class BinaryBoolean(int row, int column, IExpression left, IExpression right, BinaryType type)
    : BinaryExpr<bool, bool>(row, column, left, right)
{
    public override object Evaluate(Context context) => type switch
    {
        BinaryType.AND => (bool)Left.Evaluate(context) && (bool)Right.Evaluate(context),
        BinaryType.OR => (bool)Left.Evaluate(context) || (bool)Right.Evaluate(context),

        _ => throw new NotImplementedException(),
    };
}