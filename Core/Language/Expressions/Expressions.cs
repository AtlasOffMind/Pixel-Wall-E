using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;

public abstract class Expression<T>(int row, int column) : ASTNode(row, column), IExpression<T>
{
    public abstract T Evaluate(Context context);
}
