using System.Windows.Input;
using WidgetBoard.Data;

namespace WidgetBoard.ViewModels;

public class SettingsPageViewModel : BaseViewModel
{
    public SettingsPageViewModel(
        IPreferences preferences,
        IBoardRepository boardRepository,
        ISecureStorage secureStorage)
    {
        var lastUsedBoardId = preferences.Get("LastUsedBoardId", -1);
        if (lastUsedBoardId != -1)
        {
            LastUsedBoard = boardRepository.LoadBoard(lastUsedBoardId)?.Name ?? string.Empty;
        }

        ClearLastUsedBoardCommand = new Command(() =>
        {
            preferences.Remove("LastUsedBoardId");
            LastUsedBoard = string.Empty;
        });
        
        SaveApiTokenCommand = new Command(async () =>
        {
            await secureStorage.SetAsync("OpenWeatherApiToken", OpenWeatherApiToken);
        });

        OpenWeatherApiToken = secureStorage.GetAsync("OpenWeatherApiToken").GetAwaiter().GetResult() ?? string.Empty;
    }
    
    private string lastUsedBoard = string.Empty;
    
    public string LastUsedBoard
    {
        get => lastUsedBoard;
        set => SetProperty(ref lastUsedBoard, value);
    }
    
    public ICommand ClearLastUsedBoardCommand { get; }
    
    private string openWeatherApiToken = string.Empty;

    public string OpenWeatherApiToken
    {
        get => openWeatherApiToken;
        set => SetProperty(ref openWeatherApiToken, value);
    }

    public ICommand SaveApiTokenCommand { get; }
}