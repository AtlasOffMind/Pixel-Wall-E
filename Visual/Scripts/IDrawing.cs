namespace MyApp;

public interface IDrawing
{
    bool exist_walle { get; set; }
    Wall_e Wall_E { get; set; }
    double GetActualSize();
    void RowMapChildWallE(Wall_e wall_E);
}
