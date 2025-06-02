using Core.Interface;
using Core.Model;
using Core.Enum;

namespace Core.Language.Expressions;

public class BinaryBoolean(int row, int column, IExpression<bool> left, IExpression<bool> right, BinaryType type)
    : BinaryExpr<bool, bool>(row, column, left, right)
{
    public override bool Evaluate(Context context) => type switch
    {
        BinaryType.AND => Left.Evaluate(context) && Right.Evaluate(context),
        BinaryType.OR => throw new NotImplementedException(),

        _ => throw new NotImplementedException(),
    };
}