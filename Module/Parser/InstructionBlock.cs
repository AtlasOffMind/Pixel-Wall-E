using Core.Interface;
using Core.Model;
namespace Parser;

public class InstructionBlock(List<IInstruction> instructions) : IInstruction
{
    public List<IInstruction> Instructions { get; } = instructions;

    public void Evaluate(Context context)
    {
        foreach (var i in Instructions)
        {
            i.Evaluate(context);
        }
    }
}
