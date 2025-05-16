namespace Core.Model;

public class Context
{
    public required Dictionary<string, int> Labels { get; set; }
    public required Dictionary<string, object> Variables { get; set; }
}