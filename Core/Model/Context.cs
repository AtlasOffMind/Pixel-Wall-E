using Core.Interface;

namespace Core.Model;

public class Context
{
    public Context(IContextFunctions functions, IContextActions actions)
    {
        Functions = functions;
        Actions = actions;
    }

    public Dictionary<string, int> Labels { get; set; } = [];
    public Dictionary<string, object> Variables { get; set; } = [];
    public IContextFunctions Functions { get; set; }
    public IContextActions Actions { get; set; }

    public bool JumpCond { get; set; }
    public string? JumpTo { get; set; }
}
