namespace Core.Model;

public class FunctionsMethodInfo(Functions function, Type[] types, Type returnType)
{
    public Functions Function { get; } = function;
    public Type[] Types { get; } = types;
    public Type ReturnType { get; } = returnType;
}
