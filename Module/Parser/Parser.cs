using Core.Enum;
using Core.Interface;
using Core.Language;
using Lexer.Model;
using Core.Language.Expressions;
using Core.Extension;

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
                instructions.Add(value!);
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

    #region Line
    private void JumpingInstruct(List<Token> tokens)
    {
        while (tokens[index++].type != TokenType.BACKSLASH) ;
    }

    private bool TryIDENTIFIER(List<Token> tokens, out IInstruction? value)
    {
        var token = tokens[index - 1];
        return tokens[index++].type switch
        {
            TokenType.ASSIGN => TryAssign(tokens, token, out value),
            TokenType.OPEN_PAREN => TryMethod(tokens, token, out value),
            TokenType.BACKSLASH => TryLabel(token, out value),
            _ => throw new Exception(""),
        };
    }
    private bool TryAssign(List<Token> tokens, Token token, out IInstruction? value)
    {
        if (TryExpression(tokens, out IExpression<object>? expression))
        {
            value = new Assign(token.row, token.column, token.name, expression!);
            return true;
        }
        value = null;
        return false;
    }

    private bool TryExpression(List<Token> tokens, out IExpression<object>? expression)
    {
        if (NumericExpression(tokens, out IExpression<int>? num))
        {
            expression = num!.ToObjectExpression();
            return true;
        }
        if (BooleanExpression(tokens, out IExpression<bool>? boolean))
        {
            expression = boolean!.ToObjectExpression();
            return true;
        }
        if (ColorExpression(tokens, out IExpression<string>? str))
        {
            expression = str!.ToObjectExpression();
            return true;
        }

        expression = null;
        return false;
    }

    private bool TryLabel(Token token, out IInstruction value)
    {
        value = new Label(token.row, token.column, token.name);
        return true;
    }

    private bool TryMethod(List<Token> tokens, Token token, out IInstruction value)
    {
        List<IExpression<object>> list = [];

        if (!MatchForType(tokens, TokenType.CLOUSE_PAREN) && TryExpression(tokens, out IExpression<object>? param))
            list.Add(param!);

        while (!MatchForType(tokens, TokenType.CLOUSE_PAREN))
        {
            if (MatchForType(tokens, TokenType.COMMA) && TryExpression(tokens, out param))
                list.Add(param!);
        }

        value = new Method(token.row, token.column, token.name, list);
        return true;
    }

    private bool TryGOTO(List<Token> tokens, out IInstruction? value)
    {
        if (MatchForType(tokens, TokenType.OPEN_BRACKED)
            && MatchForType(tokens, TokenType.IDENTIFIER)
            && MatchForType(tokens, TokenType.CLOUSE_BRACKED))
        {
            var token = tokens[index - 2];
            if (MatchForType(tokens, TokenType.OPEN_PAREN)
                && BooleanExpression(tokens, out IExpression<bool>? cond)
                && MatchForType(tokens, TokenType.CLOUSE_PAREN))
            {
                value = new Goto(token.row, token.column, token.name, cond!);
                return true;
            }
        }

        // TODO Guardar exceptions
        value = null;
        return false;
    }
    #endregion

    #region Boollean Expresions
    private bool BooleanExpression(List<Token> tokens, out IExpression<bool>? expression)
    {
        return OrExpression(tokens, out expression);
    }

    private bool OrExpression(List<Token> tokens, out IExpression<bool>? expression)
    {
        if (AndExpression(tokens, out IExpression<bool>? left))
        {
            var token = tokens[index];
            if (!MatchForType(tokens, TokenType.OR))
            {
                expression = left;
                return true;
            }

            if (OrExpression(tokens, out IExpression<bool>? right))
            {
                expression = new BinaryBoolean(token.row, token.column, left!, right!, BinaryType.OR);
                return true;
            }
        }

        expression = null;
        return false;

    }

    private bool AndExpression(List<Token> tokens, out IExpression<bool>? expression)
    {
        if (LiteralExpression(tokens, TokenType.BOOLEAN, out IExpression<bool>? left))
        {
            var token = tokens[index];
            if (!MatchForType(tokens, TokenType.AND))
            {
                expression = left;
                return true;
            }

            if (AndExpression(tokens, out IExpression<bool>? right))
            {
                expression = new BinaryBoolean(token.row, token.column, left!, right!, BinaryType.AND);
                return true;
            }
        }

        expression = null;
        return false;
    }

    #endregion

    #region Numeric Expression
    private bool NumericExpression(List<Token> tokens, out IExpression<int>? num)
    {
        return SumExpression(tokens, out num);
    }

    private bool SumExpression(List<Token> tokens, out IExpression<int>? expression)
    {
        if (MultiExpression(tokens, out IExpression<int>? left))
        {
            if (SumExpression(tokens, left!, out expression))
            {
                return true;
            }
        }

        expression = null;
        return false;
    }
    private bool SumExpression(List<Token> tokens, IExpression<int> left, out IExpression<int>? expression)
    {
        List<TokenType> tokensTypes = [TokenType.PLUS, TokenType.MINUS];
        var token = tokens[index];
        if (!FirstMatch(tokens, tokensTypes, out TokenType? type))
        {
            expression = left;
            return true;
        }

        if (type == TokenType.MINUS && MultiExpression(tokens, out IExpression<int>? right))
        {
            left = new BinaryNumExpr(token.row, token.column, left!, right!, type!.Value.ToBinary());
            if (SumExpression(tokens, left, out expression))
            {
                return true;
            }
        }

        if (SumExpression(tokens, out right))
        {
            expression = new BinaryNumExpr(token.row, token.column, left!, right!, type!.Value.ToBinary());
            return true;
        }

        expression = null;
        return false;
    }

    private bool MultiExpression(List<Token> tokens, out IExpression<int>? expression)
    {
        if (PowExpression(tokens, out IExpression<int>? left))
        {
            if (MultiExpression(tokens, left!, out expression))
            {
                return true;
            }
        }

        expression = null;
        return false;
    }
    private bool MultiExpression(List<Token> tokens, IExpression<int> left, out IExpression<int>? expression)
    {
        List<TokenType> tokensTypes = [TokenType.MULTIPLICATION, TokenType.DIVISION, TokenType.MODULE];
        var token = tokens[index];
        if (!FirstMatch(tokens, tokensTypes, out TokenType? type))
        {
            expression = left;
            return true;
        }

        if (type == TokenType.DIVISION && PowExpression(tokens, out IExpression<int>? right))
        {
            left = new BinaryNumExpr(token.row, token.column, left!, right!, type!.Value.ToBinary());
            if (MultiExpression(tokens, left, out expression))
            {
                return true;
            }
        }

        if (MultiExpression(tokens, out right))
        {
            expression = new BinaryNumExpr(token.row, token.column, left!, right!, type!.Value.ToBinary());
            return true;
        }

        expression = null;
        return false;
    }

    private bool FirstMatch(List<Token> tokens, List<TokenType> tokensTypes, out TokenType? type)
    {
        //TODO ponerle el Hashtable a este for 
        foreach (var i in tokensTypes)
        {
            if (MatchForType(tokens, i))
            {
                type = i;
                return true;
            }
        }
        type = null;
        return false;
    }

    private bool PowExpression(List<Token> tokens, out IExpression<int>? expression)
    {
        if (LiteralExpression(tokens, TokenType.NUMBER, out IExpression<int>? left))
        {
            var token = tokens[index];
            if (!MatchForType(tokens, TokenType.POW))
            {
                expression = left;
                return true;
            }

            if (PowExpression(tokens, out IExpression<int>? right))
            {
                expression = new BinaryNumExpr(token.row, token.column, left!, right!, BinaryType.POW);
                return true;
            }
        }

        expression = null;
        return false;
    }
    #endregion

    private bool ColorExpression(List<Token> tokens, out IExpression<string>? str)
    {

        return LiteralExpression(tokens, TokenType.COLOR, out str);
    }

    private bool MatchForType(List<Token> tokens, TokenType type)
    {
        if (tokens[index].type != type)
            return false;
        index++;
        return true;
    }

    private bool LiteralExpression<T>(List<Token> tokens, TokenType type, out IExpression<T>? expression)
        where T : IParsable<T>
    {
        var token = tokens[index];
        if (MatchForType(tokens, type) && T.TryParse(token.name, null, out var value))
        {
            expression = new Literal<T>(value);
            return true;
        }

        expression = null;
        return false;
    }

}
