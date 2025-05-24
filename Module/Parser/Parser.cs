using Core.Enum;
using Core.Interface;
using Core.Language;
using Lexer.Model;
using Core.Language.Expressions;
using System.Diagnostics;
using Core.Model;

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
            if (MatchForType(tokens, TokenType.GOTO) && TryGOTO(tokens, out IInstruction? value))
                instructions.Add(value);
            else if (MatchForType(tokens, TokenType.IDENTIFIER) && TryIDENTIFIER(tokens, out value))
                instructions.Add(value!);
            if (MatchForType(tokens, TokenType.BACKSLASH))
                continue;

            //TODO implementar errores en cada Try y si no se encuentran ahi se lo envio desde el jumping

            JumpingInstruct(tokens);
        }

        InstructionBlock block = new(instructions);
        return block;
    }
    private bool MatchForType(List<Token> tokens, TokenType type)
    {
        if (tokens[index].type != type)
            return false;
        index++;
        return true;
    }

    private void JumpingInstruct(List<Token> tokens)
    {
        throw new NotImplementedException();
    }

    private bool TryIDENTIFIER(List<Token> tokens, out IInstruction? value)
    {
        var token = tokens[index];
        return tokens[index++].type switch
        {
            TokenType.ASSIGN => TryAssign(tokens, token, out value),
            TokenType.OPEN_PAREN => TryMethod(tokens, out value),
            TokenType.BACKSLASH => TryLabel(token, out value),
            _ => throw new Exception(""),
        };
    }
    private bool TryAssign(List<Token> tokens, Token token, out IInstruction? value)
    {
        //TODO hacer las expresiones restantes empezando por las booleanas, numericas (sin hacer las resta, la division, la potenciacion)
        if (NumericExpression(tokens, out IExpression<int> num))
        {
            value = new Assign<int>(token.row, token.column, token.name, num);
            return true;
        }
        if (BooleanExpression(tokens, out IExpression<bool>? boolean))
        {
            value = new Assign<bool>(token.row, token.column, token.name, boolean!);
            return true;
        }
        if (ColorExpression(tokens, out IExpression<string> str))
        {
            value = new Assign<string>(token.row, token.column, token.name, str);
            return true;
        }
        value = null;
        return false;
    }
    private bool BooleanExpression(List<Token> tokens, out IExpression<bool>? expression)
    {
        var temp = tokens[index];
        if (temp.name == "true")
        {
            expression = new Literal<bool>(true);
            return true;
        }
        expression = null;
        return false;
    }
    private bool ColorExpression(List<Token> tokens, out IExpression<string> str)
    {
        throw new NotImplementedException();
    }

    private bool NumericExpression(List<Token> tokens, out IExpression<int> num)
    {
        throw new NotImplementedException();
    }

    private bool TryMethod(List<Token> tokens, out IInstruction value)
    {
        throw new NotImplementedException();
    }

    private bool TryGOTO(List<Token> tokens, out IInstruction value)
    {
        throw new NotImplementedException();
    }

    private bool TryLabel(Token token, out IInstruction value)
    {
        value = new Label(token.row, token.column, token.name);
        return true;
    }


}
