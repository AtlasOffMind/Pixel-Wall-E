using Core.Enum;
using Core.Interface;
using Core.Language;
using Lexer.Model;
using Core.Language.Expressions;

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
            if (IsSeparator(tokens[index].type))
            {   //Assing
                if (tokens[index].type == TokenType.ASSIGN) instructions.Add(TryAssign(tokens[index], tokens, index));
                //Label  
                if (tokens[index].type == TokenType.IDENTIFIER) instructions.Add(TryLabel()); instructions.Add(TryMethod());
                //GoTo
                if (tokens[index].type == TokenType.GOTO) instructions.Add(TryGOTO());
                //Method
                if (MatchForType(tokens[index].type)) ;
            }
            index++;
        }

        InstructionBlock block = new(instructions);
        return block;
    }

    private bool IsSeparator(TokenType type)
    {
        return type switch
        {
            TokenType.PLUS => true,
            TokenType.POW => true,
            TokenType.MINUS => true,
            TokenType.DIVISION => true,
            TokenType.MODULE => true,
            TokenType.GREATER => true,
            TokenType.GREATER_EQUAL => true,
            TokenType.EQUAL => true,
            TokenType.EQUAL_EQUAL => true,
            TokenType.LESS => true,
            TokenType.LESS_EQUAL => true,
            TokenType.AND => true,
            TokenType.OR => true,
            _ => false,
        };
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

    private IInstruction TryAssign(Token token, List<Token> tokens, int index)
    {
        //para variables
        int i = index;
        string previous = tokens[--index].name;
        string current = tokens[i].name;
        string next = tokens[++i].name;
        Literal<int> value = new(int.Parse(next));


        Token temp = new(tokens[--index].row, tokens[--index].row, TokenType.ASSIGN, previous + current + next);
        return new Assign<int>(temp.row, temp.column, tokens[--index].name, value);
    }
}
