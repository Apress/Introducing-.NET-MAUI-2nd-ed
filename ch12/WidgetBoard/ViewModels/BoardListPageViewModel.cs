using System.Collections.ObjectModel;
using System.Windows.Input;
using WidgetBoard.Data;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class BoardListPageViewModel : BaseViewModel
{
    private readonly IBoardRepository boardRepository;
    
    public BoardListPageViewModel(IBoardRepository boardRepository)
    {
        this.boardRepository = boardRepository;
        
        AddBoardCommand = new Command(OnAddBoard);
    }
    
    public ICommand AddBoardCommand { get; }
    
    public ObservableCollection<Board> Boards { get; } = [];
    
    private Board? currentBoard;
    public Board? CurrentBoard
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

    private async void BoardSelected(Board board)
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
        TaskCompletionSource<Board?> boardCreated = new();
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
    
    public void LoadBoards()
    {
        Boards.Clear();

        var boards = this.boardRepository.ListBoards();
        foreach (var board in boards)
        {
            Boards.Add(board);
        }
    }
}