using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Color = Avalonia.Media.Color;

namespace Visual.Scripts;
public interface IDrawing
{
    bool Exist_walle { get; set; }

    Action Action { get; set; }
    Wall_e Wall_E { get; set; }
    IBrush Brush { get; set; }
    PWBrush PWBrush { get; set; }
    Rectangle[,] RectanglesMap { get; set; }

    void GetSolidColorBrush(int x, int y, out Color? color);
    void RowMapChildWallE(int x, int y);
    void Painting(int directionX, int directionY);
    double GetActualSize();
    int GetDimension();

    Color FromStringToColor(string Color);

    Location GetRealPos(int x, int y);

    Location GetRealPos(Wall_e wall_E);

}