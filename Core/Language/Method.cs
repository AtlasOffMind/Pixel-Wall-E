using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public abstract class BaseMethod(int row, int column, string name, List<IExpression> @params) : ASTNode(row, column), ISemantic
{
    public string Name { get; } = name;
    public IExpression[] Params { get; } = [.. @params];

    public virtual IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        ActionsMethodInfo? action = null;
        if (!context.Functions.GetMethodInfo(Name, out var function) && !context.Actions.GetMethodInfo(Name, out action))
            yield return new SemanticError(Location, "The method dosen't exist in the current context");
        else if (function is not null && function.Types.Length != Params.Length)
            yield return new SemanticError(Location, "The Function's elements are not correct");
        else if (action is not null && action.Types.Length != Params.Length)
            yield return new SemanticError(Location, "The Action's elements are not correct");
    }
}

public class Method<T>(int row, int column, string name, List<IExpression> @params) : BaseMethod(row, column, name, @params), IExpression
{
    public object Evaluate(Context context)
    {
        context.Functions.GetMethodInfo(Name, out var function);
        return function!.Function([.. Params.Select(x => x.Evaluate(context))]);
    }
}

public class Method(int row, int column, string name, List<IExpression> @params) : BaseMethod(row, column, name, @params), IInstruction
{
    public void SearchLabels(Context context) { }
    public void Evaluate(Context context)
    {
        context.Actions.GetMethodInfo(Name, out var methodInfo);
        methodInfo!.Action([.. Params.Select(x => x.Evaluate(context))]);
    }
}