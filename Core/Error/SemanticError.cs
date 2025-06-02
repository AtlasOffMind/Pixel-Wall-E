using Core.Model;

namespace Core.Error;

public class SemanticError : PixelWallyErrors
{
    public SemanticError(Location location, string? message) : base(location, message) { }

    public SemanticError(Location location, string? message, Exception? innerException) : base(location, message, innerException) { }
}
