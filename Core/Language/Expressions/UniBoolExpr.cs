using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language.Expressions;


public class UniBoolExpr(int row, int column, IExpression left) : UnitaryExpr<bool>(row, column, left)
{
    public override IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        return Left.CheckSemantic(context);
    }

    public override object Evaluate(Context context) =>
        Left.Evaluate(context) is bool cond ?
        !cond : throw new SemanticError(new Location(Row, Column), "Bool Expected for the operation");
}


public class UniNumExpr(int row, int column, IExpression left) : UnitaryExpr<int>(row, column, left)
{
    public override object Evaluate(Context context) => -(int)Left.Evaluate(context);
}