using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Variable(int row, int column, string name) : ASTNode(row, column), IExpression
{
    public string Name { get; } = name;

    public bool CheckSemantic(Context context)
        => context.Variables.TryGetValue(Name, out object? _);

    public object Evaluate(Context context) => context.Variables[Name];
}