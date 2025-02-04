using System.Collections.ObjectModel;
using System.Windows.Input;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class BoardListPageViewModel : BaseViewModel
{
    public BoardListPageViewModel()
    {
        Boards.Add(
            new FixedBoard
            {
                Name = "My first board",
                NumberOfColumns = 3,
                NumberOfRows = 2
            });
        
        AddBoardCommand = new Command(OnAddBoard);
    }
    
    public ICommand AddBoardCommand { get; }
    
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
                { "Board", board}
            });
    }
    
    private async void OnAddBoard()
    {
        TaskCompletionSource<FixedBoard?> boardCreated = new();
        await Shell.Current.GoToAsync(
            RouteNames.BoardDetails,
            new Dictionary<string, object>
            {
                { "Created", boardCreated }
            });
    
        var newBoard = await boardCreated.Task;
    
        if (newBoard is not null)
        {
            Boards.Add(newBoard);
        }
    }
}