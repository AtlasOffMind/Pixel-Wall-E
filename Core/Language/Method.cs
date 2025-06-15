using Core.Language.Expressions;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public abstract class BaseMethod(int row, int column, string name, List<IExpression> @params) : ASTNode(row, column), ISemantic
{
    public string Name { get; } = name;
    public IExpression[] Params { get; } = [.. @params];


    public virtual bool CheckSemantic(Context context)
    {
        if (context.Functions.GetMethodInfo(Name, out var function))
            return function.Types.Length == Params.Length;
        return context.Actions.GetMethodInfo(Name, out var action)
            && action.Types.Length == Params.Length;
    }
}

public class Method<T>(int row, int column, string name, List<IExpression> @params) : BaseMethod(row, column, name, @params), IExpression
{
    public object Evaluate(Context context)
    {
        context.Functions.GetMethodInfo(Name, out var function);
        return (T)function.Function([.. Params.Select(x => x.Evaluate(context))]);
    }

    public override bool CheckSemantic(Context context)
        => base.CheckSemantic(context)
        && context.Functions.GetMethodInfo(Name, out var methodInfo)
        && methodInfo.ReturnType == typeof(T);
}

public class Method(int row, int column, string name, List<IExpression> @params) : BaseMethod(row, column, name, @params), IInstruction
{
    public void SearchLabels(Context context) { }
    public void Evaluate(Context context)
    {
        context.Actions.GetMethodInfo(Name, out var methodInfo);
        methodInfo.Action([.. Params.Select(x => x.Evaluate(context))]);
    }
}