using Core.Interface;
using Core.Model;
using Lexer.Model;
namespace Parser;

public class Parser()
{
    public int index;
    public IInstruction Parse(List<Token> tokens)
    {
        index = 0;
        return InstructBlock(tokens);
    }

    public IInstruction InstructBlock(List<Token> tokens)
    {
        List<IInstruction> instructions = [];
        while (index < tokens.Count)
        {

        }
        //Assing
        //Method
        //Label  
        //GoTo
    }
}

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
