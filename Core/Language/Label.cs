using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Label(int row, int column, string name) : ASTNode(row, column), IInstruction
{
    public string Name { get; } = name;

    public bool CheckSemantic(Context context)
    {
        if (context.Labels.ContainsKey(Name))
            return false;
        context.Labels[Name] = Location.Row;
        return true;
    }

    //
    public void Evaluate(Context context) => context.Labels[Name] = Location.Row;
}
