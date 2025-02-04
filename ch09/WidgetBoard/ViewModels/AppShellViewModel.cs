using System.Collections.ObjectModel;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    public AppShellViewModel()
    {
        Boards.Add(
            new FixedBoard
            {
                Name = "My first board",
                NumberOfColumns = 3,
                NumberOfRows = 2
            });
    }
    
    public ObservableCollection<FixedBoard> Boards { get; } = [];
    
    private FixedBoard? currentBoard;
    public FixedBoard? CurrentBoard
    {
        get => currentBoard;
        set
        {
            if (SetProperty(ref currentBoard, value) && 
                value is not null)
            {
                BoardSelected(value);
            }
        }
    }

    private async void BoardSelected(FixedBoard board)
    {
        await Shell.Current.GoToAsync(
            RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", board }
            });
    }
}