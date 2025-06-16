namespace Core.Model;

public class ActionsMethodInfo(Actions action, Type[] types)
{
    public Actions Action { get; } = action;
    public Type[] Types { get; } = types;
}