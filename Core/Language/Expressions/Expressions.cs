using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class Expression<T>(int row, int column) : ASTNode(row, column), IExpression
{
    public abstract IEnumerable<SemanticError> CheckSemantic(Context context);

    public abstract object Evaluate(Context context);
}
