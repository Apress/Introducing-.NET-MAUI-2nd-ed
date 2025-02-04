using WidgetBoard.Models;

namespace WidgetBoard;

public class BoardSearchHandler : SearchHandler
{
    private readonly IList<FixedBoard> boards =
    [
        new FixedBoard
        {
            Name = "My first board"
        },
        new FixedBoard
        {
            Name = "My second board"
        },
        new FixedBoard
        {
            Name = "My third board",
        }
    ];
    
    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = boards
                .Where(board => board.Name.Contains(newValue, StringComparison.CurrentCultureIgnoreCase))
                .ToList<FixedBoard>();
        }
    }
    
    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete
        await Task.Delay(1000);
    
        await Shell.Current.GoToAsync(
            RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", (FixedBoard)item}
            });
    }
}