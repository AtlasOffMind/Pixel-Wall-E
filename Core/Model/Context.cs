namespace Core.Model;

public class Context
{
    public Context(IContextFunctions functions, IContextActions actions)
    {
        Functions = functions;
        Actions = actions;
    }

    public Context()
    {
       
    }

    public Dictionary<string, int> Labels { get; set; } = [];
    public Dictionary<string, object> Variables { get; set; } = [];
    public IContextFunctions Functions { get; set; }
    public IContextActions Actions { get; set; }

    public bool JumpCond { get; set; }
    public string? JumpTo { get; set; }
}

public interface IContextFunctions
{
    bool GetMethodInfo(string name, out FunctionsMethodInfo? methodInfo);
}

public interface IContextActions
{
    bool GetMethodInfo(string name, out ActionsMethodInfo? methodInfo);
}

public class FunctionsMethodInfo(Functions function, Type[] types, Type returnType)
{
    public Functions Function { get; } = function;
    public Type[] Types { get; } = types;
    public Type ReturnType { get; } = returnType;
}

public class ActionsMethodInfo(Actions action, Type[] types)
{
    public Actions Action { get; } = action;
    public Type[] Types { get; } = types;
}