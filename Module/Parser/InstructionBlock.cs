using Core.Interface;
using Core.Model;
namespace Parser;

public class InstructionBlock(List<IInstruction> instructions) : IInstruction
{
    public List<IInstruction> Instructions { get; } = instructions;

    public bool CheckSemantic(Context context)
    {
        bool result = true;
        for (var i = 0; i < Instructions.Count; i++)
        {
            result &= Instructions[i].CheckSemantic(context);
        }
        return result;
    }

    public void Evaluate(Context context)
    {
        foreach (var i in Instructions)
        {
            i.Evaluate(context);
        }
    }
}
