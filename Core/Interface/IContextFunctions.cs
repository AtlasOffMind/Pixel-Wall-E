using Core.Model;

namespace Core.Interface;

public interface IContextFunctions
{
    bool GetMethodInfo(string name, out FunctionsMethodInfo? methodInfo);
}
