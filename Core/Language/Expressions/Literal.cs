using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Literal<T>(int row, int column, T value) : Expression<T>(row, column), IExpression<T>
{
    public T Value { get; set; } = value;

    public override T Evaluate(Context context)
    {
        return Value;
    }
}