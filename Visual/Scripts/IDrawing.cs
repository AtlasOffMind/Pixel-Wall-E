using System.Drawing;
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
    //Rectangle[,] RectanglesMap { get; set; }

    void GetSolidColorBrush(int x, int y, out Color? color);

    //setear a walle como hijo del roadmap
    void RowMapChildWallE(Wall_e wall_E);

    double GetActualSize();
    int GetDimension();

}