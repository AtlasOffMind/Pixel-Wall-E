namespace Core.Language;

public struct Location(int row, int column)
{
    public int Row = row;
    public int Column = column;
}

