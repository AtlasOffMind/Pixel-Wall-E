using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.AST.Expressions;

public class BinaryCompareExpr(int row, int column, IExpression<int> left, IExpression<int> right, BinaryType type) : BinaryExpr<int>(row, column, left, right)
{
    public override int Evaluate(Context context) => type switch
    {
        BinaryType.EQUAL_EQUAL => Left.Evaluate(context) == Right.Evaluate(context) ? 1 : 0,
        BinaryType.DIFERENT =>  Left.Evaluate(context) != Right.Evaluate(context) ? 1 : 0,
        BinaryType.GREATER =>  Left.Evaluate(context) > Right.Evaluate(context) ? 1 : 0,
        BinaryType.LESS =>  Left.Evaluate(context) < Right.Evaluate(context) ? 1 : 0,
        BinaryType.GREATER_EQUAL =>  Left.Evaluate(context) >= Right.Evaluate(context) ? 1 : 0,
        BinaryType.LESS_EQUAL =>  Left.Evaluate(context) <= Right.Evaluate(context) ? 1 : 0,

        _ => throw new NotImplementedException(),

    };
}