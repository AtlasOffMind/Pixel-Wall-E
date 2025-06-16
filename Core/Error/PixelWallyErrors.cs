using Core.Model;

namespace Core.Error;

public abstract class PixelWallyErrors : Exception
{
    public Location Location { get; set; }

    public PixelWallyErrors(Location location, string? message)
        : base(message) => Location = location;

    public PixelWallyErrors(Location location, string? message, Exception? innerException)
        : base(message, innerException) => Location = location;

    public override string ToString()
    {
        return $"{GetType().Name}({Location.Row},{Location.StartColumn}:{Location.EndColumn}) => {Message}";
    }
}