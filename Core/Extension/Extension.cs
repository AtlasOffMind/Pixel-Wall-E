using Core.Enum;
using Core.Interface;
using Core.Model;

namespace Core.Extension;

public static class Extensions
{
    public static BinaryType ToBinary(this TokenType type) => type switch
    {
        TokenType.PLUS => BinaryType.SUM,
        TokenType.MINUS => BinaryType.MINUS,
        TokenType.DIVISION => BinaryType.DIVISION,
        TokenType.MULTIPLICATION => BinaryType.MULTIPLICATION,
        TokenType.POW => BinaryType.POW,
        TokenType.MODULE => BinaryType.MODULE,
        TokenType.EQUAL_EQUAL => BinaryType.EQUAL_EQUAL,
        TokenType.GREATER => BinaryType.GREATER,
        TokenType.GREATER_EQUAL => BinaryType.GREATER_EQUAL,
        TokenType.LESS => BinaryType.LESS,
        TokenType.LESS_EQUAL => BinaryType.LESS_EQUAL,
        TokenType.DIFERENT => BinaryType.DIFERENT,
        TokenType.AND => BinaryType.AND,
        TokenType.OR => BinaryType.OR,
        _ => throw new NotImplementedException()
    };

    public static IExpression<object> ToObjectExpression<T>(this IExpression<T> expression)
        => new ObjectExpression<T>(expression);
}

public class ObjectExpression<T>(IExpression<T> expression) : IExpression<object>
{
    public IExpression<T> Expression { get; } = expression;

    public bool CheckSemantic(Context context)
    {
        return Expression.CheckSemantic(context);
    }

    public object Evaluate(Context context)
    {
        return Expression.Evaluate(context)!;
    }
}