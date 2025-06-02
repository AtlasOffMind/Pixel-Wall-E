using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Variable<T>(int row, int column, string name) : ASTNode(row, column), IExpression<T>
{
    public string Name { get; } = name;

    public bool CheckSemantic(Context context)
        => context.Variables.TryGetValue(Name, out object? value) && value is T;

    public T Evaluate(Context context) => (T)context.Variables[Name];
}