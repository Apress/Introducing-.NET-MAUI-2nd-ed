using SQLite;

namespace WidgetBoard.Models;

public class Board
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public int NumberOfColumns { get; init; }
    public int NumberOfRows { get; init; }
    
    [Ignore]
    public IReadOnlyList<BoardWidget> BoardWidgets { get; set; } = [];
}