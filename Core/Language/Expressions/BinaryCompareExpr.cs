using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.Expressions;

public class BinaryCompareExpr(int row, int column, IExpression left, IExpression right, BinaryType type)
    : BinaryExpr<bool, int>(row, column, left, right)
{
    public override object Evaluate(Context context) => type switch
    {
        BinaryType.EQUAL_EQUAL => (int)Left.Evaluate(context) == (int)Right.Evaluate(context),
        BinaryType.DIFERENT => (int)Left.Evaluate(context) != (int)Right.Evaluate(context),
        BinaryType.GREATER => (int)Left.Evaluate(context) > (int)Right.Evaluate(context),
        BinaryType.LESS => (int)Left.Evaluate(context) < (int)Right.Evaluate(context),
        BinaryType.GREATER_EQUAL => (int)Left.Evaluate(context) >= (int)Right.Evaluate(context),
        BinaryType.LESS_EQUAL => (int)Left.Evaluate(context) <= (int)Right.Evaluate(context),

        _ => throw new NotImplementedException(),

    };
}