using Core.Model;

namespace Core.Error;

public class GramaticError : PixelWallyErrors
{
    public GramaticError(Location location, string? message) : base(location, message) { }

    public GramaticError(Location location, string? message, Exception? innerException) : base(location, message, innerException) { }
}
