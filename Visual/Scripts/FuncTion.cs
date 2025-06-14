using System;
using Avalonia.Media;


namespace Visual.Scripts;
public class FuncTion(IDrawing drawing)
{
    // Retorna el valor X de la posici´ on actual de Wall-E.
    public int GetActualX()
    {
        var i = drawing.Action.GetRealPos(drawing.Wall_E);
        return i.x;
    }
    // Retorna el valor Y de la posicion actual de Wall-E.
    public int GetActualY()
    {
        var i = drawing.Action.GetRealPos(drawing.Wall_E);
        return i.y;
    }
    // Retorna tamaño largo y ancho del canvas. Para un canvas de n×n se retorna n.
    public int GetCanvasSize()
    {
        return drawing.GetDimension();
    }
    public int GetColorCount(string color, int x1, int y1, int x2, int y2)
    {
        int count = 0;
        for (int i = x1; i < x2; i++)
            for (int j = y1; j < y2; j++)
            {
                drawing.GetSolidColorBrush(i, j, out Color? temp);
                if (temp == drawing.FromStringToColor(color))
                    count++;
            }


        return count;
    }

    //Retorna 1 si el color de la brocha actual es string color, 0 en caso contrario.
    public int IsBrushColor(string color)
    {
        return drawing.PWBrush.CurrentColor == drawing.FromStringToColor(color) ? 1 : 0;
    }
    //Retorna 1 si el tamaño de la brocha actual es size, 0 en caso contrario.
    public int IsBrushSize(int size)
    {
        return drawing.PWBrush.Size == size ? 1 : 0;
    }
    public int IsCanvasColor(string color, int vertical, int horizontal)
    {
        Location location = drawing.Action.GetRealPos(drawing.Wall_E);
        location.x += horizontal;
        location.y += vertical;

        drawing.GetSolidColorBrush(location.x, location.y, out Color? temp);

        return temp == drawing.FromStringToColor(color) ? 1 : 0;


    }
    
}