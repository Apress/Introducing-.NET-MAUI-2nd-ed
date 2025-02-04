using System.Collections.ObjectModel;
using WidgetBoard.Models;

namespace WidgetBoard;

public class BoardSearchHandler : SearchHandler
{
    public static readonly BindableProperty BoardsProperty =
        BindableProperty.Create(
            nameof(Boards),
            typeof(ObservableCollection<Board>),
            typeof(BoardSearchHandler));

    public ObservableCollection<Board> Boards
    {
        get => (ObservableCollection<Board>)GetValue(BoardsProperty);
        set => SetValue(BoardsProperty, value);
    }
    
    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Boards
                .Where(board => board.Name.Contains(newValue, StringComparison.CurrentCultureIgnoreCase))
                .ToList<Board>();
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
                { "Board", (Board)item}
            });
    }
}