namespace Core.Model;

public class Context
{
    public Dictionary<string, int> Labels { get; set; } = [];
    public Dictionary<string, object> Variables { get; set; } = [];
}