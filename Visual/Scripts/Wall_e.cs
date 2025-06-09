using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Visual.Scripts;
public class Wall_e()
{
    public Image walleImage = new()
    {
        Source = new Bitmap(@"C:\My things\Git\2nd Proyect Pixel Wall-E\Visual\Assets\Icons\Wall_E.png"),
        Width = 20,
        Height = 20,
        Stretch = Stretch.Uniform
    };

    public int rowPos;
    public int colPos;

}
