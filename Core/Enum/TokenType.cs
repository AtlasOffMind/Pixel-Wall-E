namespace Core.Enum;

public enum TokenType
{
    // Single-character tokens.
    PLUS, MINUS,
    DIVISION, MULTIPLICATION, POW,
    OPEN_PAREN, CLOUSE_PAREN, MODULE,

    // LITERALS.
    IDENTIFIER, NUMBER, LABEL,

    // One or two character tokens.
    BANG, BANG_EQUAL,
    EQUAL, EQUAL_EQUAL,
    GREATER, GREATER_EQUAL,
    LESS, LESS_EQUAL,
    DIFERENT,

    // KEYWORDS.
    AND, FALSE,
    GOTO, OR, TRUE,
    EOF, ASSIGN,
    BACKSLASH, COLOR,
    BOOLEAN,
}