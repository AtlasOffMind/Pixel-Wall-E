using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Variable<T>(int row, int column, string name) : Expression<T>(row, column), IExpression<T>
{
    public string Name { get; } = name;

    public override T Evaluate(Context context)
    {
        return (T)context.Variables[Name];
    }
}