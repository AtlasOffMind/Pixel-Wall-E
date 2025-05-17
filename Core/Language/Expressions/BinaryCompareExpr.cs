using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.Expressions;

public class BinaryCompareExpr(int row, int column, IExpression<int> left, IExpression<int> right, BinaryType type) : BinaryExpr<bool, int>(row, column, left, right)
{
    public override bool Evaluate(Context context) => type switch
    {
        BinaryType.EQUAL_EQUAL => Left.Evaluate(context) == Right.Evaluate(context),
        BinaryType.DIFERENT => Left.Evaluate(context) != Right.Evaluate(context),
        BinaryType.GREATER => Left.Evaluate(context) > Right.Evaluate(context),
        BinaryType.LESS => Left.Evaluate(context) < Right.Evaluate(context),
        BinaryType.GREATER_EQUAL => Left.Evaluate(context) >= Right.Evaluate(context),
        BinaryType.LESS_EQUAL => Left.Evaluate(context) <= Right.Evaluate(context),

        _ => throw new NotImplementedException(),

    };
}