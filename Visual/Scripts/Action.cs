using System;
using Avalonia.Controls;

namespace MyApp;

public class Action(IDrawing drawing)
{
    private Location GetRealPos(int x, int y)
    {
        var actualSize = drawing.GetActualSize();
        return new Location(x * (int)actualSize, y * (int)actualSize);
    }

    private Location GetRealPos(Wall_e wall_E)
    {
        var actualSize = drawing.GetActualSize();
        return new Location(wall_E.colPos * (int)actualSize, wall_E.rowPos * (int)actualSize);
    }

    public void Spawn(int x, int y)
    {
        if (!drawing.exist_walle)
        {
            //TODO si esto no lo uso quitarlo hasta del objeto
            drawing.Wall_E.rowPos = y;
            drawing.Wall_E.colPos = x;

            var temPos = GetRealPos(x, y);
            Canvas.SetLeft(drawing.Wall_E.walleImage, temPos.y);
            Canvas.SetTop(drawing.Wall_E.walleImage, temPos.x);
            drawing.RowMapChildWallE(drawing.Wall_E);

            drawing.exist_walle = true;
        }
        else
        {
            throw new NotImplementedException("This method can only be used 1 time");
        }
    }
}
