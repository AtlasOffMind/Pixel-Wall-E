using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Label(int row, int column, string name) : ASTNode(row, column), IInstruction
{
    public string Name { get; } = name;

    public void SearchLabels(Context context)
        => context.Labels[Name] = Location.Row;

    public bool CheckSemantic(Context context)
        => context.Labels.TryGetValue(Name, out int value) && value == Location.Row;

    public void Evaluate(Context context) { }

}
