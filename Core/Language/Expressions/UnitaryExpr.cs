using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class UnitaryExpr<T>(int row, int column, IExpression<T> left) : IExpression<T>
{
    public int Row { get; } = row;
    public int Column { get; } = column;
    public IExpression<T> Left { get; } = left;
    public bool CheckSemantic(Context context) => Left.CheckSemantic(context);
    public abstract T Evaluate(Context context);
}
