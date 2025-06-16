using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public class Literal<T>(int row, int column, T value) : ASTNode(row, column), IExpression
{
    public T Value { get; set; } = value;

    public IEnumerable<SemanticError> CheckSemantic(Context context) => [];

    public object Evaluate(Context context) => Value!;
}