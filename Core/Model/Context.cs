namespace Core.Model;

public class Context(Dictionary<string, FunctionsMethodInfo> functions, Dictionary<string, ActionsMethodInfo> actions)
{
    public Dictionary<string, int> Labels { get; set; } = [];
    public Dictionary<string, object> Variables { get; set; } = [];
    public Dictionary<string, FunctionsMethodInfo> Functions { get; set; } = functions;
    public Dictionary<string, ActionsMethodInfo> Actions { get; set; } = actions;

    public bool JumpCond { get; set; }
    public string? JumpTo { get; set; }
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