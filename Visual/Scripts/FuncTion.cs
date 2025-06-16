using System;
using System.Linq;
using Avalonia.Media;
using Core.Interface;
using Core.Model;

namespace Visual.Scripts;
public class FuncTion(IDrawing drawing) : IContextFunctions
{
    // Retorna el valor X de la posicion actual de Wall-E.
    public int GetActualX()
    {
        return drawing.Wall_E.colPos;
    }
    // Retorna el valor Y de la posicion actual de Wall-E.
    public int GetActualY()
    {
        return drawing.Wall_E.rowPos;
    }
    // Retorna tamaño largo y ancho del canvas. Para un canvas de n×n se retorna n.
    public int GetCanvasSize()
    {
        return drawing.GetDimension();
    }
    public int GetColorCount(string color, int x1, int y1, int x2, int y2)
    {
        Color tempColor;
        Color compareColor = drawing.FromStringToColor(color);

        int count = 0;
        for (int i = x1; i < x2; i++)
            for (int j = y1; j < y2; j++)
            {
                drawing.GetSolidColorBrush(i, j, out tempColor);
                if (tempColor == compareColor)
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
        var x = drawing.Wall_E.colPos + horizontal;
        var y = drawing.Wall_E.rowPos + vertical;

        drawing.GetSolidColorBrush(x, y, out Color temp);
        return temp == drawing.FromStringToColor(color) ? 1 : 0;
    }

    public bool GetMethodInfo(string name, out FunctionsMethodInfo? methodInfo)
    {
        methodInfo = null;
        var method = GetType().GetMethods().FirstOrDefault(x => x.Name == name);
        if (method == null)
            return false;

        object functions(object[] x) => method.Invoke(this, x)!;
        Type[] types = [.. method.GetParameters().Select(x => x.ParameterType)];
        methodInfo = new FunctionsMethodInfo(functions, types, method.ReturnType);
        return false;
    }
}