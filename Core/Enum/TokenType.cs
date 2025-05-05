namespace Core.Enum;

public enum TokenType
{
    // Single-character tokens.
    Plus, Minus,
    Division, Multiplication,
    Open_Paren, Clouse_Paren,

    // Literals.
    Identifier, NUMBER, Label,

    // One or two character tokens.
    Bang, Bang_Equal,
    Equal, Equal_Equal,
    Greater, Greater_Equal,
    Less, Less_Equal,

    // Keywords.
    AND, ELSE, FALSE, FUN, FOR,
    GOTO, If, OR, True, 
    EOF,

}