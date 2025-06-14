using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Visual.Scripts;
using Action = Visual.Scripts.Action;
using Location = Visual.Scripts.Location;

namespace MyApp;
public partial class MainWindow : Window, IDrawing
{
    public bool Exist_walle { get; set; }
    private double CellSize { get; set; }
    public Wall_e Wall_E { get; set; }
    public Rectangle[,] RectanglesMap { get; set; }
    public PWBrush PWBrush { get; set; }
    public IBrush Brush { get; set; }

    public Action Action { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        InstantiatingVariables();
        DrawingRoadMap();
    }

    private void InstantiatingVariables()
    {
        var dimesion = (int)CanvasResize.Value!;
        CellSize = double.Min(RoadMap.Width, RoadMap.Height) / dimesion;
        RectanglesMap = new Rectangle[dimesion, dimesion];
        Wall_E = new Wall_e();
        Exist_walle = false;
        PWBrush = new PWBrush(Colors.White, 1);
        Action = new Action(this);

    }

    private void DrawingRoadMap()
    {
        var actualSize = CellSize * ZoomButton.Value * 0.01;
        var dimesion = (int)CanvasResize.Value!;
        RectanglesMap = new Rectangle[dimesion, dimesion];
        RoadMap.Height = actualSize * dimesion;
        RoadMap.Width = actualSize * dimesion;

        for (int i = 0; i < dimesion; i++)
            for (int j = 0; j < dimesion; j++)
                DrawingRectangle(i, j, actualSize);
    }

    private void DrawingRectangle(int i, int j, double actualSize)
    {
        var cell = new Rectangle()
        {
            Width = actualSize,
            Height = actualSize,
            Stroke = Brushes.Black,
            StrokeThickness = 0.2,
            Fill = new SolidColorBrush(Colors.White),
        };

        Canvas.SetLeft(cell, actualSize * j);
        Canvas.SetTop(cell, actualSize * i);
        RoadMap.Children.Add(cell);
        RectanglesMap[i, j] = cell;
    }

    //TODO aun no se como hacer que pinte
    public void Painting(int directionX, int directionY)
    {
        if (Exist_walle && PWBrush.Size > 0)
        {
            RowMapChildWallE(Wall_E.colPos + directionX, Wall_E.rowPos + directionY);
            RectanglesMap[Wall_E.colPos, Wall_E.rowPos].Fill = new SolidColorBrush(PWBrush.CurrentColor);
            var size = PWBrush.Size - 1;

            if (directionX * directionY == 0)
            {
                size /= 2;
                var dirx = (directionX + 1) % 2;
                var diry = (directionY + 1) % 2;

                DrawBrushWidth(dirx, diry, size);
                DrawBrushWidth(-dirx, -diry, size);
            }
            else
            {
                var dirx = directionX;
                var diry = directionY;

                DrawBrushWidth(dirx, 0, size);
                DrawBrushWidth(0, diry, size);
            }
        }
        else if (!Exist_walle)
        {
            throw new NotImplementedException("There is no Wall-E in the current context");
        }
    }

    public void DrawBrushWidth(int dirx, int diry, int distance)
    {
        (int x, int y) = (Wall_E.colPos, Wall_E.rowPos);
        for (int i = 1; i <= distance; i++)
        {
            (int newX, int newY) = (x + dirx * i, y + diry * i);
            RectanglesMap[newX, newY].Fill = new SolidColorBrush(PWBrush.CurrentColor);
        }
    }

    public void GetSolidColorBrush(int x, int y, out Color? color)
    {
        color = null;
        if (RectanglesMap[x, y].Fill is SolidColorBrush brush)
        {
            color = brush.Color;
        }

    }

    public Color FromStringToColor(string Color)
    {
        Color CurrentColor = new();

        CurrentColor = Color switch
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

        return CurrentColor;
    }

    public void RowMapChildWallE(int x, int y)
    {
        var location = GetRealPos(x, y);
        Canvas.SetLeft(Wall_E.walleImage, location.x);
        Canvas.SetTop(Wall_E.walleImage, location.y);

        if (!Exist_walle)
            RoadMap.Children.Add(Wall_E.walleImage);
    }


    public int GetDimension() => (int)CanvasResize.Value!;
    public double GetActualSize() => CellSize * ZoomButton.Value * 0.01;

    public Location GetRealPos(int x, int y)
    {
        var actualSize = GetActualSize();
        return new Location(x * (int)actualSize, y * (int)actualSize);
    }

    public Location GetRealPos(Wall_e wall_E)
    {
        var actualSize = GetActualSize();
        return new Location(wall_E.colPos * (int)actualSize, wall_E.rowPos * (int)actualSize);
    }
}

public partial class MainWindow : Window
{
    public void ResizeButtonOnClick(object sender, RoutedEventArgs e)
    {
        RoadMap.Children.Clear();
        DrawingRoadMap();
    }

    public void ZoomSlideOnChange(object sender, RoutedEventArgs e)
    {
        var actualSize = CellSize * ZoomButton.Value * 0.01;
        RoadMap.Height = actualSize * (double)CanvasResize.Value!;
        RoadMap.Width = actualSize * (double)CanvasResize.Value!;
        for (int i = 0; i < RoadMap.Children.Count; i++)
        {
            var item = RoadMap.Children[i] as Rectangle;
            var col = Canvas.GetLeft(item!) / item!.Width;
            var row = Canvas.GetTop(item!) / item!.Height;

            item!.Width = actualSize;
            item!.Height = actualSize;
            Canvas.SetLeft(item, actualSize * col);
            Canvas.SetTop(item, actualSize * row);
        }
    }



}
