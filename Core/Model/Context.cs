namespace Core.Model;

public class Context(Dictionary<string, Functions> functions, Dictionary<string, Actions> actions)
{
    public Dictionary<string, int> Labels { get; set; } = [];
    public Dictionary<string, object> Variables { get; set; } = [];
    public Dictionary<string, Functions> Functions { get; set; } = functions;
    public Dictionary<string, Actions> Actions { get; set; } = actions;

    public bool JumpCond { get; set; }
    public string? JumpTo { get; set; }
}
