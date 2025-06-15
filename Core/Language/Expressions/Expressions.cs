using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class Expression<T>(int row, int column) : ASTNode(row, column), IExpression
{
    public abstract bool CheckSemantic(Context context);

    public abstract object Evaluate(Context context);
}
