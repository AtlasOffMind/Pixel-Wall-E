using Core.Interface;
using Core.Model;
namespace Core.Language;

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
}
