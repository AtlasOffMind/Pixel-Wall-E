using System;
using Avalonia.Media;
using Core.Model;
using System.Linq;
using Core.Interface;

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
                throw new InvalidOperationException("Invalid position");
            }
        }
        else
        {
            throw new InvalidOperationException("This method can only be used 1 time");
        }
    }

    public void Color(string Color)
    {
        string color = Color.Substring(1, Color.Length - 2);
        Color actualColorBrush = drawing.PWBrush.CurrentColor;
        drawing.Brush = new SolidColorBrush(drawing.PWBrush.CurrentColor);

        drawing.PWBrush.CurrentColor = drawing.FromStringToColor(color);

        if (actualColorBrush != drawing.PWBrush.CurrentColor)
            drawing.Brush = new SolidColorBrush(drawing.PWBrush.CurrentColor);
    }

    public void Size(int k)
    {
        if (k > 0)
            drawing.PWBrush.Size = k - (k + 1) % 2;

        else throw new InvalidOperationException("The brush size must be a number higher than 0");
    }

    public void DrawLine(int dirX, int dirY, int distance)
    {
        dirX = int.Sign(dirX);
        dirY = int.Sign(dirY);
        int size = drawing.PWBrush.Size;

        for (int i = 0; i < distance; i++)
        {
            if (dirX * dirY != 0 && i < size)
                drawing.PWBrush.Size = i;
            drawing.Painting(dirX, dirY);
            drawing.RowMapChildWallE(drawing.Wall_E.colPos + dirX, drawing.Wall_E.rowPos + dirY);
        }
        drawing.PWBrush.Size = size;
    }

    public void DrawEasterEgg(int dirX, int dirY, int radius)
    {
        dirX = int.Sign(dirX);
        dirY = int.Sign(dirY);

        int p = 1 - radius;
        int xc = drawing.Wall_E.colPos + dirX * radius;
        int yc = drawing.Wall_E.rowPos + dirY * radius;

        for (int x = 0, y = radius; x < y; x++)
        {
            drawing.RowMapChildWallE(xc + x, yc + y);
            if (p < 0)
            {
                p += 2 * x + 1;
            }
            else
            {
                y--;
                p += 2 * (x - y) + 1;
            }

            drawing.DrawEasterEgg(xc, yc, x, y);
        }
        drawing.RowMapChildWallE(xc, yc);
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
            drawing.PaintingBlock(xc + x, yc + y);
            drawing.PaintingBlock(xc - x, yc + y);
            drawing.PaintingBlock(xc + x, yc - y);
            drawing.PaintingBlock(xc - x, yc - y);
            drawing.PaintingBlock(xc + y, yc + x);
            drawing.PaintingBlock(xc - y, yc + x);
            drawing.PaintingBlock(xc + y, yc - x);
            drawing.PaintingBlock(xc - y, yc - x);
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
            (int x, int y) = (midX - width + 1, midY - height + 1);
            int corr = (drawing.PWBrush.Size - 1) / 2;
            drawing.RowMapChildWallE(x, y);
            foreach (var item in dir)
            {
                drawing.RowMapChildWallE(drawing.Wall_E.colPos - item.x * corr, drawing.Wall_E.rowPos - item.y * corr);
                var length = item.x != 0 ? width : height;
                length = 2 * length - 1 + corr;
                DrawLine(item.x, item.y, length);
                drawing.RowMapChildWallE(drawing.Wall_E.colPos - item.x, drawing.Wall_E.rowPos - item.y);
            }
            drawing.RowMapChildWallE(midX, midY);
        }
    }

    public void Fill()
    {
        var mask = new bool[drawing.RectanglesMap.GetLength(0), drawing.RectanglesMap.GetLength(0)];
        (int x, int y) = (drawing.Wall_E.colPos, drawing.Wall_E.rowPos);
        drawing.GetSolidColorBrush(x, y, out Color color);
        var dirs = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        Fill(mask, x, y, color, dirs);
    }

    public void Fill(bool[,] mask, int x, int y, Color color, (int, int)[] dirs)
    {
        drawing.GetSolidColorBrush(x, y, out Color neighborColor);
        if (mask[x, y] || !drawing.IsValidPos(x, y) || neighborColor != color)
            return;
        mask[x, y] = true;
        drawing.RectanglesMap[x, y].Fill = drawing.Brush;
        foreach (var (dx, dy) in dirs)
        {
            int newX = x + dx, newY = y + dy;
            Fill(mask, newX, newY, color, dirs);
        }
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
        return true;
    }

    public void RePos(int dirX, int dirY) => drawing.RowMapChildWallE(dirX, dirY);

}
