using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using WidgetBoard.Data;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public class FixedBoardPageViewModel : BaseViewModel, IQueryAttributable
{
    public FixedBoardPageViewModel(
        WidgetTemplateSelector widgetTemplateSelector,
        WidgetFactory widgetFactory,
        IBoardRepository boardRepository,
        IPreferences preferences)
    {
        this.widgetFactory = widgetFactory;
        this.boardRepository = boardRepository;
        this.preferences = preferences;
        WidgetTemplateSelector = widgetTemplateSelector;
        Widgets = [];
        
        AddWidgetCommand = new Command(OnAddWidget);
        AddNewWidgetCommand = new Command<int>(index =>
        {
            IsAddingWidget = true;
            addingPosition = index;
        });
    }
    
    private string boardName = string.Empty;
    private int numberOfColumns;
    private int numberOfRows;
    private int addingPosition;
    private string? selectedWidget;
    private bool isAddingWidget;
    private readonly WidgetFactory widgetFactory;
    private Board? board;
    private readonly IBoardRepository boardRepository;
    private readonly IPreferences preferences;

    public IList<string> AvailableWidgets => widgetFactory.AvailableWidgets;

    public ICommand AddWidgetCommand { get; }
    public ICommand AddNewWidgetCommand { get; }

    public bool IsAddingWidget
    {
        get => isAddingWidget;
        set => SetProperty(ref isAddingWidget, value);
    }

    public string? SelectedWidget
    {
        get => selectedWidget;
        set => SetProperty(ref selectedWidget, value);
    }


    public string BoardName
    {
        get => boardName;
        set => SetProperty(ref boardName, value);
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

    public ObservableCollection<IWidgetViewModel> Widgets { get; }

    public WidgetTemplateSelector WidgetTemplateSelector { get; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var boardParameter = (Board)query["Board"];
        board = boardRepository.LoadBoard(boardParameter.Id);

        if (board is not null)
        {
            preferences.Set("LastUsedBoardId", board.Id);
            
            BoardName = board.Name;
            NumberOfColumns = board.NumberOfColumns;
            NumberOfRows = board.NumberOfRows;
            
            foreach (var boardWidget in board.BoardWidgets)
            {
                var widgetViewModel = widgetFactory.CreateWidgetViewModel(boardWidget.WidgetType);

                if (widgetViewModel is null)
                {
                    continue;
                }
                
                widgetViewModel.Position = boardWidget.Position;
                Widgets.Add(widgetViewModel);
            }
        }
    }
    
    private void OnAddWidget()
    {
        if (SelectedWidget is null)
        {
            return;
        }

        var widgetViewModel = widgetFactory.CreateWidgetViewModel(SelectedWidget);

        if (widgetViewModel is not null)
        {
            widgetViewModel.Position = addingPosition;
            Widgets.Add(widgetViewModel);
            
            SaveWidget(widgetViewModel);
        }

        IsAddingWidget = false;
    }
    
    private void SaveWidget(IWidgetViewModel widgetViewModel)
    {
        if (board is null)
        {
            return;
        }
        
        var boardWidget = new BoardWidget
        {
            BoardId = board.Id,
            Position = widgetViewModel.Position,
            WidgetType = widgetViewModel.Type
        };
        boardRepository.CreateBoardWidget(boardWidget);
    }

}