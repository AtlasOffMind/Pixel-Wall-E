using Core.Model;

namespace Core.Interface;

public interface IContextActions
{
    bool GetMethodInfo(string name, out ActionsMethodInfo? methodInfo);
}
