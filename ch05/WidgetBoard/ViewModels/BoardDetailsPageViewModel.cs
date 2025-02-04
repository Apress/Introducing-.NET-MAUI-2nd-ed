using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class BoardDetailsPageViewModel : BaseViewModel
{
    private string boardName = string.Empty;
    private bool isFixed = true;
    private int numberOfColumns = 3;
    private int numberOfRows = 2;
    
    public string BoardName
    {
        get => boardName;
        set
        {
            SetProperty(ref boardName, value);
            SaveCommand.ChangeCanExecute();
        }
    }
    
    public bool IsFixed
    {
        get => isFixed;
        set => SetProperty(ref isFixed, value);
    }
    
    public int NumberOfColumns
    {
        get => numberOfColumns;
        set => SetProperty(ref numberOfColumns, value);
    }
    
    public int NumberOfRows
    {
        get => numberOfRows;
        set => SetProperty(ref numberOfRows, value);
    }

    public Command SaveCommand { get; }
    
    public BoardDetailsPageViewModel()
    {
        SaveCommand = new Command(
            () => Save(),
            () => !string.IsNullOrWhiteSpace(BoardName));
    }
    private void Save()
    {
        var board = new FixedBoard
        {
            Name = BoardName,
            NumberOfColumns = NumberOfColumns,
            NumberOfRows = NumberOfRows
        };
    }

}