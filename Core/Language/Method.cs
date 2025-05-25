using Core.Language.Expressions;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Method<T>(int row, int column, string name, List<IExpression<object>> @params) : Expressions<T>(row, column), IExpression<T>
{
    public string Name { get; } = name;
    public IExpression<object>[] Params { get; } = [.. @params];

    public override T Evaluate(Context context) => (T)context.Functions[Name]([.. Params.Select(x => x.Evaluate(context))]);
}

public class Method(int row, int column, string name, List<IExpression<object>> @params) : ASTNode(row, column), IInstruction
{
    public string Name { get; } = name;
    public IExpression<object>[] Params { get; } = [.. @params];

    public bool CheckSemantic(Context context)
    {
        // TODO hacer checkeo de metodo
        //context.Actions[Name].Method.GetParameters();
        throw new NotImplementedException();
    }

    public void Evaluate(Context context) => context.Actions[Name]([.. Params.Select(x => x.Evaluate(context))]);
}