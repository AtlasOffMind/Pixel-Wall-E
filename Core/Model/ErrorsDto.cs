using Core.Error;

namespace Core.Model;

public class ErrorsDto()
{
    public List<SyntaxError> Lexer { get; set; } = [];
    public List<GramaticError> Parser { get; set; } = [];
    public List<SemanticError> SemanticErrors { get; set; } = [];

}