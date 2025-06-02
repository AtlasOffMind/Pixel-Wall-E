namespace Core.Model;

public struct Location
{
    public int Row;
    public int StartColumn;
    public int EndColumn;

    public Location(int row, int column)
    {
        Row = row;
        StartColumn = column;
        EndColumn = column + 1;
    }
    public Location(int row, int startColumn, int endColumn)
    {
        Row = row;
        StartColumn = startColumn;
        EndColumn = endColumn;
    }
}
