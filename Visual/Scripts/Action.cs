using System;
using Avalonia.Media;
using Avalonia.Controls;
using Core.Model;
using System.Linq;

namespace Visual.Scripts;
public class Action(IDrawing drawing) : IContextActions
{
    public void Spawn(int x, int y)
    {
        if (!drawing.Exist_walle)
        {
            if (drawing.IsValidPos(x, y))
            {
                drawing.RowMapChildWallE(x, y);
                drawing.Exist_walle = true;
            }
            else
            {
                throw new NotImplementedException("Invalid position");
            }
        }
        else
        {
            throw new NotImplementedException("This method can only be used 1 time");
        }
    }

    public void Color(string Color)
    {
        string color = Color.Substring(1, Color.Length - 2);
        Color actualColorBrush = drawing.PWBrush.CurrentColor;
        drawing.Brush = new SolidColorBrush(drawing.PWBrush.CurrentColor);

        drawing.PWBrush.CurrentColor = color switch
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
        if (k > 0)
            drawing.PWBrush.Size = k - (k + 1) % 2;

        else throw new NotImplementedException("The brush size must be a number higher than 0");
    }

    public void DrawLine(int dirX, int dirY, int distance)
    {
        dirX = int.Sign(dirX);
        dirY = int.Sign(dirY);
        int size = drawing.PWBrush.Size;

        drawing.RectanglesMap[drawing.Wall_E.colPos, drawing.Wall_E.rowPos].Fill = new SolidColorBrush(drawing.PWBrush.CurrentColor);

        for (int i = 0; i < distance; i++)
        {
            if (distance - i < drawing.PWBrush.Size)
                drawing.PWBrush.Size = distance - i;
            drawing.Painting(dirX, dirY);
        }
        drawing.PWBrush.Size = size;
    }

    public void DrawCircle(int dirX, int dirY, int radius)
    {
        dirX = int.Sign(dirX);
        dirY = int.Sign(dirY);

        int p = 1 - radius;
        int xc = drawing.Wall_E.colPos + dirX * radius;
        int yc = drawing.Wall_E.rowPos + dirY * radius;

        for (int x = 0, y = radius; x < y; x++)
        {
            if (p < 0)
            {
                p += 2 * x + 1;
            }
            else
            {
                y--;
                p += 2 * (x - y) + 1;
            }

            drawing.DibujarOctantes(xc, yc, x, y);
        }
        drawing.RowMapChildWallE(xc, yc);
    }

    public void DrawRectangle(int dirX, int dirY, int distance, int width, int height)
    {
        if (drawing.IsValidPos(dirX, dirY))
        {
            (int x, int y)[] dir = [(1, 0), (0, 1), (-1, 0), (0, -1)];
            dirX = int.Sign(dirX);
            dirY = int.Sign(dirY);
            var midX = drawing.Wall_E.colPos + dirX * distance;
            var midY = drawing.Wall_E.rowPos + dirY * distance;

            // Punto A del rectangulo 
            (int x, int y) = (drawing.Wall_E.colPos - width, drawing.Wall_E.rowPos - height);

            drawing.RowMapChildWallE(x, y);
            foreach (var item in dir)
            {
                var length = item.x != 0 ? width : height;
                length = 2 * length - 1;
                DrawLine(item.x, item.y, length);
            }
            drawing.RowMapChildWallE(midX, midY);
        }
    }

    public void Fill()
    {

    }

    public bool GetMethodInfo(string name, out ActionsMethodInfo? methodInfo)
    {
        methodInfo = null;
        var method = GetType().GetMethods().FirstOrDefault(x => x.Name == name);
        if (method == null)
            return false;

        void actions(object[] x) => method.Invoke(this, x);
        Type[] types = [.. method.GetParameters().Select(x => x.ParameterType)];
        methodInfo = new ActionsMethodInfo(actions, types);
        return false;
    }
}
