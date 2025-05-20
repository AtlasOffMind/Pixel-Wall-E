using System.Text.RegularExpressions;
using Core.Enum;
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

    public InstructionBlock InstructBlock(List<Token> tokens)
    {
        List<IInstruction> instructions = [];
        while (index < tokens.Count)
        {
        //Assing
            if( tokens[index].type == TokenType.ASSIGN) instructions.Add(TryAssign());
        //Label  
            if( tokens[index].type == TokenType.IDENTIFIER) instructions.Add(TryLabel());  instructions.Add(TryMethod());
        //GoTo
            if( tokens[index].type == TokenType.GOTO) instructions.Add(TryGOTO());
        //Method
            if(MatchForType(tokens[index].type)) 
            index++;
        }

        InstructionBlock block = new(instructions);
        return block;
    }

    private IInstruction TryMethod()
    {
        throw new NotImplementedException();
    }

    private IInstruction TryGOTO()
    {
        throw new NotImplementedException();
    }

    private IInstruction TryLabel()
    {
        throw new NotImplementedException();
    }

    private bool MatchForType(TokenType type)
    {
        //para operaciones aritmeticas
        throw new NotImplementedException();
    }

    private IInstruction TryAssign()
    {
        //para variables
        throw new NotImplementedException();
    }
}
