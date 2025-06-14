using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Core.Model;
using Lexer;
using Visual.Scripts;
using Action = Visual.Scripts.Action;
using Location = Visual.Scripts.Location;

//using MessageBox.Avalonia.Enums;
//using MessageBox.Avalonia;

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
    public FuncTion FuncTion { get; set; }

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
        FuncTion = new FuncTion(this);
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

        Canvas.SetLeft(cell, actualSize * i);
        Canvas.SetTop(cell, actualSize * j);
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
        Color CurrentColor = Color switch
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

        Wall_E.colPos = x;
        Wall_E.rowPos = y;
        Canvas.SetLeft(Wall_E.walleImage, location.x);
        Canvas.SetTop(Wall_E.walleImage, location.y);

        if (Exist_walle)
            return;
        Wall_E.walleImage.Width = GetActualSize();
        Wall_E.walleImage.Height = GetActualSize();
        RoadMap.Children.Add(Wall_E.walleImage);
    }


    public int GetDimension() => (int)CanvasResize.Value!;
    public double GetActualSize() => CellSize * ZoomButton.Value * 0.01;

    public Location GetRealPos(int x, int y)
    {
        var actualSize = GetActualSize();
        return new Location(x * actualSize, y * actualSize);
    }

    public Location GetRealPos(Wall_e wall_E)
    {
        var actualSize = GetActualSize();
        return new Location(wall_E.colPos * (int)actualSize, wall_E.rowPos * (int)actualSize);
    }

    public bool IsValidPos(int x, int y) => (x >= 0 && x < GetDimension() && y >= 0 && y < GetDimension()) ? true : false;

    public void DibujarOctantes(int xc, int yc, int x, int y)
    {
        Painting(xc + x, yc + y);
        Painting(xc - x, yc + y);
        Painting(xc + x, yc - y);
        Painting(xc - x, yc - y);
        Painting(xc + y, yc + x);
        Painting(xc - y, yc + x);
        Painting(xc + y, yc - x);
        Painting(xc - y, yc - x);
    }

    public void ClearCanvas()
    {
        foreach (var item in RoadMap.Children)
        {
            if (item is not Rectangle rectangle)
                continue;
            rectangle.Fill = Brushes.White;
        }
        RoadMap.Children.Remove(Wall_E.walleImage);
        Exist_walle = false;
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
            var item = RoadMap.Children[i];
            var col = Canvas.GetLeft(item!) / item!.Width;
            var row = Canvas.GetTop(item!) / item!.Height;

            item!.Width = actualSize;
            item!.Height = actualSize;
            Canvas.SetLeft(item, actualSize * col);
            Canvas.SetTop(item, actualSize * row);
        }
    }

    public void Execute_Click(object sender, RoutedEventArgs e)
    {
        ClearCanvas();

        var parser = new Parser.Parser();
        var context = new Context(FuncTion, Action);

        var code = TextEditor.Text;
        var lines = code!.Split("\n");
        var tokens = Scanner.Tokenizer(lines);
        var ast = parser.Parse(tokens);

        ast.SearchLabels(context);
        ast.CheckSemantic(context);
        try
        {
            ast.Evaluate(context);
        }
        catch (Exception ex)
        {
            ex = ex is TargetInvocationException target ? target.InnerException! : ex;

        }
    }

    public async void ToSave(object sender, RoutedEventArgs e)
    {
        var dir = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory);
        var options = new FilePickerSaveOptions()
        {
            DefaultExtension = ".pw",
            SuggestedStartLocation = dir,
        };

        var text = TextEditor.Text;
        var storages = await StorageProvider.SaveFilePickerAsync(options);
        var stream = await storages!.OpenWriteAsync();
        using var writer = new StreamWriter(stream);
        writer.WriteLine(text);

    }
    public async void ToLoad(object sender, RoutedEventArgs e)
    {
        var dir = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory);
        var options = new FilePickerOpenOptions()
        {
            FileTypeFilter = [new FilePickerFileType("*.pw")],
            SuggestedStartLocation = dir
        };
        var storages = await StorageProvider.OpenFilePickerAsync(options);
        var file = storages.FirstOrDefault();
        var stream = await file!.OpenReadAsync();
        using var reader = new StreamReader(stream);
        TextEditor.Text = reader.ReadToEnd();
    }
}
