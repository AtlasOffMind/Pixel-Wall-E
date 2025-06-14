namespace Core.Model;

public class Context(IContextFunctions functions, IContextActions actions)
{
    public Dictionary<string, int> Labels { get; set; } = [];
    public Dictionary<string, object> Variables { get; set; } = [];
    public IContextFunctions Functions { get; set; } = functions;
    public IContextActions Actions { get; set; } = actions;

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