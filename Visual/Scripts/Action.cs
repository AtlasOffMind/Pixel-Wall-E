using System;
using Avalonia.Media;
using Avalonia.Controls;

namespace Visual.Scripts;
public class Action(IDrawing drawing)
{
    public Location GetRealPos(int x, int y)
    {
        var actualSize = drawing.GetActualSize();
        return new Location(x * (int)actualSize, y * (int)actualSize);
    }

    public Location GetRealPos(Wall_e wall_E)
    {
        var actualSize = drawing.GetActualSize();
        return new Location(wall_E.colPos * (int)actualSize, wall_E.rowPos * (int)actualSize);
    }

    public void Spawn(int x, int y)
    {
        if (!drawing.Exist_walle)
        {
            //TODO si esto no lo uso quitarlo hasta del objeto
            drawing.Wall_E.rowPos = y;
            drawing.Wall_E.colPos = x;

            var temPos = GetRealPos(x, y);
            Canvas.SetLeft(drawing.Wall_E.walleImage, temPos.y);
            Canvas.SetTop(drawing.Wall_E.walleImage, temPos.x);
            drawing.RowMapChildWallE(drawing.Wall_E);

            drawing.Exist_walle = true;
        }
        else
        {
            throw new NotImplementedException("This method can only be used 1 time");
        }
    }

    public void Color(string Color)
    {
        Color actualColorBrush = drawing.PWBrush.CurrentColor;
        drawing.Brush = new SolidColorBrush(drawing.PWBrush.CurrentColor);

        drawing.PWBrush.CurrentColor = Color switch
        {
            "Red" => Colors.Red,
            "Blue" => Colors.Blue,
            "Green" => Colors.Green,
            "Yellow" => Colors.Yellow,
            "Orange" => Colors.Orange,
            "Purple" => Colors.Purple,
            "Black" => Colors.Black,
            "White" => Colors.White,
            "Transparent" => Colors.Transparent,
            _ => throw new NotImplementedException("that's not a valid color"),
        };

        if (actualColorBrush != drawing.PWBrush.CurrentColor)
            drawing.Brush = new SolidColorBrush(drawing.PWBrush.CurrentColor);
    }

    public void Size(int k)
    {
        drawing.PWBrush.Size = k;
    }

    public void DrawLine(int dirX, int dirY, int distance) { }

    public void DrawCircle(int dirX, int dirY, int radius) { }

    public void DrawRectangle(int dirX, int dirY, int distance, int width, int height) { }
    public void Fill() { }
    //Esto modifica el color del pincel existente y afectará todo lo que lo esté usando (por ejemplo, si varias celdas comparten el mismo pincel, cambiará en todas).
    //     var brush = Brush as SolidColorBrush;
    // if (brush != null)
    // {
    //     brush.Color = Colors.Red;
    // }

    //Esto es para dibujar los rectangulos en un color X
    // var celda = new Rectangle
    // {
    //     Width = 50,
    //     Height = 50,
    //     Fill = _colorSeleccionado // el pincel
    // };
}
