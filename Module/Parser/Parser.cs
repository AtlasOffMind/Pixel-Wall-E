using Core.Enum;
using Core.Interface;
using Core.Language;
using Lexer.Model;
using Core.Language.Expressions;
using Core.Extension;
using Core.Error;

namespace Parser;

public class Parser()
{
    public int index;
    public List<GramaticError> exceptions = [];

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
            else if (MatchForType(tokens, TokenType.IDENTIFIER))
            {
                if (TryIDENTIFIER(tokens, out value))
                    instructions.Add(value!);
                else
                    exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), "Assign, Parenthesis or Backslash expected"));
            }
            else
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
        value = null;
        return tokens[index++].type switch
        {
            TokenType.ASSIGN => TryAssign(tokens, token, out value),
            TokenType.OPEN_PAREN => TryMethod(tokens, token, out value),
            TokenType.BACKSLASH => TryLabel(token, out value),
            _ => false
        };
    }
    private bool TryAssign(List<Token> tokens, Token token, out IInstruction? value)
    {
        if (TryExpression(tokens, out IExpression? expression, TokenType.BACKSLASH))
        {
            value = new Assign(token.row, token.column, token.name, expression!);
            return true;
        }
        exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), ""));
        value = null;
        return false;
    }

    private bool TryExpression(List<Token> tokens, out IExpression? expression, TokenType? endType = null)
    {
        var startIndex = index;
        if (BooleanExpression(tokens, out expression) && (endType == null || MatchForType(tokens, endType.Value)))
        {
            return true;
        }

        index = startIndex;

        if (NumericExpression(tokens, out expression) && (endType == null || MatchForType(tokens, endType.Value)))
        {
            return true;
        }

        index = startIndex;

        if (ColorExpression(tokens, out expression) && (endType == null || MatchForType(tokens, endType.Value)))
        {
            return true;
        }

        expression = null;
        index = startIndex;
        return false;
    }

    private bool TryLabel(Token token, out IInstruction value)
    {
        value = new Label(token.row, token.column, token.name);
        return true;
    }

    private bool CheckMethod(List<Token> tokens, Token token, out List<IExpression>? list)
    {
        list = [];
        IExpression? param;
        if (tokens[index].type != TokenType.CLOUSE_PAREN)
        {
            if (TryExpression(tokens, out param))
                list.Add(param!);
            else
                return false;
        }

        while (!MatchForType(tokens, TokenType.CLOUSE_PAREN))
        {
            if (!MatchForType(tokens, TokenType.COMMA))
            {
                exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), "Comma expected"));
                return false;
            }
            else if (!TryExpression(tokens, out param))
                return false;
            list.Add(param!);
        }

        return true;
    }

    private bool TryMethod(List<Token> tokens, Token token, out IInstruction? value)
    {
        if (!CheckMethod(tokens, token, out List<IExpression>? list) | !MatchForType(tokens, TokenType.BACKSLASH))
        {
            value = null;
            return false;
        }
        value = new Method(token.row, token.column, token.name, list!);
        return true;
    }


    private bool TryMethod<T>(List<Token> tokens, Token token, out IExpression? value)
    {
        if (!MatchForType(tokens, TokenType.OPEN_PAREN) || !CheckMethod(tokens, token, out List<IExpression>? list))
        {
            value = null;
            return false;
        }
        value = new Method<T>(token.row, token.column, token.name, list!);
        return true;
    }


    private bool TryGOTO(List<Token> tokens, out IInstruction? value)
    {
        if (!MatchForType(tokens, TokenType.OPEN_BRACKED))
        {
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " '{' or '[' expected"));
            value = null;
            return false;
        }

        if (!MatchForType(tokens, TokenType.IDENTIFIER))
        {
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " 'IDENTIFIER' expected"));
            value = null;
            return false;
        }
        if (!MatchForType(tokens, TokenType.CLOUSE_BRACKED))
        {
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " '}' or ']' expected"));
            value = null;
            return false;
        }

        var token = tokens[index - 2];

        if (!MatchForType(tokens, TokenType.OPEN_PAREN))
        {
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " '{' or '[' expected"));
            value = null;
            return false;
        }
        if (!BooleanExpression(tokens, out IExpression? cond))
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " bool expected"));
        if (!MatchForType(tokens, TokenType.CLOUSE_PAREN))
        {
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " '}' or ']' expected"));
            value = null;
            return false;
        }
        if (!MatchForType(tokens, TokenType.BACKSLASH))
        {
            exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), " Backslash expected"));
            value = null;
            return false;
        }

        value = new Goto(token.row, token.column, token.name, cond!);
        return true;
    }
    #endregion

    #region Boollean Expresions
    private bool BooleanExpression(List<Token> tokens, out IExpression? expression)
    {
        return OrExpression(tokens, out expression);
    }

    private bool OrExpression(List<Token> tokens, out IExpression? expression)
    {
        var startIndex = index;
        if (AndExpression(tokens, out IExpression? left))
        {
            var token = tokens[index];
            if (!MatchForType(tokens, TokenType.OR))
            {
                expression = left;
                return true;
            }

            if (OrExpression(tokens, out IExpression? right))
            {
                expression = new BinaryBoolean(token.row, token.column, left!, right!, BinaryType.OR);
                return true;
            }
        }
        else exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), ""));

        expression = null;
        index = startIndex;
        return false;

    }

    private bool AndExpression(List<Token> tokens, out IExpression? expression)
    {
        var startIndex = index;
        if (ComparerExpression(tokens, out IExpression? left) || LiteralExpression<bool>(tokens, TokenType.BOOLEAN, out left))
        {
            var token = tokens[index];
            if (!MatchForType(tokens, TokenType.AND))
            {
                expression = left;
                return true;
            }

            if (AndExpression(tokens, out IExpression? right))
            {
                expression = new BinaryBoolean(token.row, token.column, left!, right!, BinaryType.AND);
                return true;
            }
        }
        else exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), ""));


        expression = null;
        index = startIndex;
        return false;
    }

    private bool ComparerExpression(List<Token> tokens, out IExpression? expression)
    {
        var startIndex = index;
        List<TokenType> types = [TokenType.LESS, TokenType.GREATER, TokenType.EQUAL_EQUAL, TokenType.LESS_EQUAL, TokenType.GREATER_EQUAL, TokenType.DIFERENT];
        if (NumericExpression(tokens, out IExpression? left))
        {
            var token = tokens[index];
            if (FirstMatch(tokens, types, out TokenType? type) && NumericExpression(tokens, out IExpression? right))
            {
                expression = new BinaryCompareExpr(token.row, token.column, left!, right!, type!.Value.ToBinary());
                return true;
            }
        }
        else exceptions.Add(new GramaticError(LocationFactory.Create(tokens[index]), ""));


        expression = null;
        index = startIndex;
        return false;
    }

    #endregion

    #region Numeric Expression
    private bool NumericExpression(List<Token> tokens, out IExpression? num)
    {
        return SumExpression(tokens, out num);
    }

    private bool SumExpression(List<Token> tokens, out IExpression? expression)
    {
        var startIndex = index;
        if (MultiExpression(tokens, out IExpression? left))
        {
            if (SumExpression(tokens, left!, out expression))
            {
                return true;
            }
        }

        expression = null;
        index = startIndex;
        return false;
    }
    private bool SumExpression(List<Token> tokens, IExpression left, out IExpression? expression)
    {
        var startIndex = index;
        List<TokenType> tokensTypes = [TokenType.PLUS, TokenType.MINUS];
        var token = tokens[index];
        if (!FirstMatch(tokens, tokensTypes, out TokenType? type))
        {
            expression = left;
            return true;
        }

        if (type == TokenType.MINUS && MultiExpression(tokens, out IExpression? right))
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
        index = startIndex;
        return false;
    }

    private bool MultiExpression(List<Token> tokens, out IExpression? expression)
    {
        var startIndex = index;
        if (PowExpression(tokens, out IExpression? left))
        {
            if (MultiExpression(tokens, left!, out expression))
            {
                return true;
            }
        }

        expression = null;
        index = startIndex;
        return false;
    }
    private bool MultiExpression(List<Token> tokens, IExpression left, out IExpression? expression)
    {
        List<TokenType> tokensTypes = [TokenType.MULTIPLICATION, TokenType.DIVISION, TokenType.MODULE];
        var startIndex = index;
        var token = tokens[index];
        if (!FirstMatch(tokens, tokensTypes, out TokenType? type))
        {
            expression = left;
            return true;
        }

        if (type == TokenType.DIVISION && PowExpression(tokens, out IExpression? right))
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
        index = startIndex;
        return false;
    }

    private bool FirstMatch(List<Token> tokens, List<TokenType> tokensTypes, out TokenType? type)
    {
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

    private bool PowExpression(List<Token> tokens, out IExpression? expression)
    {
        var startIndex = index;
        if (UniNumExpression(tokens, out IExpression? left))
        {
            var token = tokens[index];
            if (!MatchForType(tokens, TokenType.POW))
            {
                expression = left;
                return true;
            }

            if (PowExpression(tokens, out IExpression? right))
            {
                expression = new BinaryNumExpr(token.row, token.column, left!, right!, BinaryType.POW);
                return true;
            }
        }

        expression = null;
        index = startIndex;
        return false;
    }

    private bool UniNumExpression(List<Token> tokens, out IExpression? expression)
    {
        int count = 0;
        while (MatchForType(tokens, TokenType.MINUS))
            count++;
        Token token = tokens[index - 1];
        if (!LiteralExpression<int>(tokens, TokenType.NUMBER, out expression))
            return false;
        if (count % 2 != 0)
            expression = new UniNumExpr(token.row, token.column, expression!);
        return true;
    }
    #endregion

    private bool ColorExpression(List<Token> tokens, out IExpression? str)
    {
        return LiteralExpression<string>(tokens, TokenType.COLOR, out str);
    }

    private bool MatchForType(List<Token> tokens, TokenType type)
    {
        if (tokens[index].type != type)
            return false;
        index++;
        return true;
    }

    private bool LiteralExpression<T>(List<Token> tokens, TokenType type, out IExpression? expression)
        where T : IParsable<T>
    {
        var startIndex = index;
        var token = tokens[index];
        if (MatchForType(tokens, type) && T.TryParse(token.name, null, out var value))
        {
            expression = new Literal<T>(token.row, token.column, value);
            return true;
        }
        if (MatchForType(tokens, TokenType.IDENTIFIER))
        {
            if (!TryMethod<T>(tokens, token, out expression))
                expression = new Variable(token.row, token.column, token.name);
            return true;
        }
        expression = null;
        index = startIndex;
        return false;
    }

}
