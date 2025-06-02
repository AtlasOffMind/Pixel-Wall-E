using Core.Model;

namespace Core.Error;

public class SyntaxError : PixelWallyErrors
{
    public SyntaxError(Location location, string? message) : base(location, message) { }
    public SyntaxError(Location location, string? message, Exception? innerException) : base(location, message, innerException) { }
}
