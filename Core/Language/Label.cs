using Core.Error;
using Core.Interface;
using Core.Model;

namespace Core.Language;

public class Label(int row, int column, string name) : ASTNode(row, column), IInstruction
{
    public string Name { get; } = name;

    public void SearchLabels(Context context)
        => context.Labels[Name] = Location.Row;

    public IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        if (context.Labels.TryGetValue(Name, out int value) && value != Location.Row)
            yield return new SemanticError(Location, "Already exist the Label");
    }


    public void Evaluate(Context context) { }

}
