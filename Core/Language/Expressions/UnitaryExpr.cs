using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;


public abstract class UnitaryExpr<T>(int row, int column, IExpression left) : IExpression
{
    public int Row { get; } = row;
    public int Column { get; } = column;
    public IExpression Left { get; } = left;
    public abstract object Evaluate(Context context);

    public virtual IEnumerable<SemanticError> CheckSemantic(Context context) => Left.CheckSemantic(context);
}
