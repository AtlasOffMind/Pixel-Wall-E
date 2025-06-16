using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Variable(int row, int column, string name) : ASTNode(row, column), IExpression
{
    public string Name { get; } = name;

    public IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        if (context.Variables.TryGetValue(Name, out object? _))
            yield return new SemanticError(Location, "");
    }

    public object Evaluate(Context context) => context.Variables[Name];
}