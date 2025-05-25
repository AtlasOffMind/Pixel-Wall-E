using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Literal<T>(T value) : IExpression<T>
{
    public T Value { get; set; } = value;

    public T Evaluate(Context context)
    {
        return Value;
    }
}