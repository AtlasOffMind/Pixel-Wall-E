using Core.Error;
using Core.Interface;
using Core.Model;
namespace Core.Language;

public class InstructionBlock(List<IInstruction> instructions) : IInstruction
{
    public List<IInstruction> Instructions { get; } = instructions;

    public IEnumerable<SemanticError> CheckSemantic(Context context)
    {
        for (var i = 0; i < Instructions.Count; i++)
        {
            foreach (var item in Instructions[i].CheckSemantic(context))
            {
                yield return item;
            }
        }
    }

    public void Evaluate(Context context)
    {
        for (int i = 0; i < Instructions.Count; i++)
        {
            Instructions[i].Evaluate(context);
            if (context.JumpCond)
            {
                i = context.Labels[context.JumpTo!] - 1;
                context.JumpCond = false;
            }
        }
    }

    public void SearchLabels(Context context)
    {
        foreach (var item in Instructions)
        {
            item.SearchLabels(context);
        }
    }
}
