using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class UnitaryExpr<T>(int row, int column, IExpression left) : IExpression
{
    public int Row { get; } = row;
    public int Column { get; } = column;
    public IExpression Left { get; } = left;
    public bool CheckSemantic(Context context) => Left.CheckSemantic(context);
    public abstract object Evaluate(Context context);
}
