using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;


namespace MyApp;

public partial class MainWindow : Window
{
    public Wall_e wall_E;
    private double cellSize;
    private Rectangle[,] RectanglesMap;

    public MainWindow()
    {
        InitializeComponent();
        InstantiatingVariables();
        DrawingRoadMap();
    }

    private void InstantiatingVariables()
    {
        var dimesion = (int)CanvasResize.Value!;
        cellSize = double.Min(RoadMap.Width, RoadMap.Height) / dimesion;
        RectanglesMap = new Rectangle[dimesion, dimesion];
        wall_E = new Wall_e();

    }

    private void DrawingRoadMap()
    {
        var actualSize = cellSize * ZoomButton.Value * 0.01;
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
            StrokeThickness = 0.2
        };

        Canvas.SetLeft(cell, actualSize * j);
        Canvas.SetTop(cell, actualSize * i);
        RoadMap.Children.Add(cell);
        RectanglesMap[i, j] = cell;
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
        var actualSize = cellSize * ZoomButton.Value * 0.01;
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
