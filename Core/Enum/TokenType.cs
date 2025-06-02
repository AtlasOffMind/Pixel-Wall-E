namespace Core.Enum;

public enum TokenType
{
    // Single-character tokens.
    PLUS, MINUS,
    DIVISION, MULTIPLICATION, POW,
    OPEN_PAREN, CLOUSE_PAREN, MODULE,
    CLOUSE_BRACKED, OPEN_BRACKED,

    // LITERALS.
    IDENTIFIER, NUMBER,

    // One or two character tokens.
    EQUAL, EQUAL_EQUAL,
    GREATER, GREATER_EQUAL,
    LESS, LESS_EQUAL,
    DIFERENT,

    // KEYWORDS.
    AND,
    GOTO, OR,
    EOF, ASSIGN,
    BACKSLASH, COLOR,
    BOOLEAN,
    COMMA,

}