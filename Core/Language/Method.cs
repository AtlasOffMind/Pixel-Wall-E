using Core.Language.Expressions;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public abstract class BaseMethod(int row, int column, string name, List<IExpression<object>> @params) : ASTNode(row, column), ISemantic
{
    public string Name { get; } = name;
    public IExpression<object>[] Params { get; } = [.. @params];

    // TODO hacer q matcheen los tipos de Params con los parametros de las funciones
    public virtual bool CheckSemantic(Context context)
    {
        if (context.Functions.TryGetValue(Name, out FunctionsMethodInfo? function))
            return function.Types.Length == Params.Length;
        return context.Actions.TryGetValue(Name, out ActionsMethodInfo? action)
            && action.Types.Length == Params.Length;
    }
}

public class Method<T>(int row, int column, string name, List<IExpression<object>> @params) : BaseMethod(row, column, name, @params), IExpression<T>
{
    public T Evaluate(Context context) => (T)context.Functions[Name].Function([.. Params.Select(x => x.Evaluate(context))]);

    public override bool CheckSemantic(Context context)
    {
        return base.CheckSemantic(context) && context.Functions[Name].ReturnType == typeof(T);
    }
}

public class Method(int row, int column, string name, List<IExpression<object>> @params) : BaseMethod(row, column, name, @params), IInstruction
{
    public void SearchLabels(Context context) { }
    public void Evaluate(Context context) => context.Actions[Name].Action([.. Params.Select(x => x.Evaluate(context))]);
}