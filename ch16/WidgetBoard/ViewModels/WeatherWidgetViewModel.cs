using System.Windows.Input;
using WidgetBoard.Communications;
using WidgetBoard.Services;

namespace WidgetBoard.ViewModels;

public class WeatherWidgetViewModel : BaseViewModel, IWidgetViewModel
{
    public const string DisplayName = "Weather";
    public int Position { get; set; }
    public string Type => DisplayName;
    
    private readonly IWeatherForecastService weatherForecastService;
    private readonly ISecureStorage secureStorage;
    private readonly ILocationService locationService;

    private State state;
    public State State
    {
        get => state;
        set => SetProperty(ref state, value);
    }

    public ICommand LoadWeatherCommand { get; }

    public WeatherWidgetViewModel(
        IWeatherForecastService weatherForecastService,
        ISecureStorage secureStorage,
        ILocationService locationService)
    {
        this.weatherForecastService = weatherForecastService;
        this.secureStorage = secureStorage;
        this.locationService = locationService;
        Task.Run(async () => await LoadWeatherForecast());
        LoadWeatherCommand = new Command(async () => await LoadWeatherForecast());
    }

    public async Task LoadWeatherForecast()
    {
        var apiKey = await this.secureStorage.GetAsync("OpenWeatherApiToken");

        if (apiKey is null)
        {
            return;
        }
        
        try
        {
            State = State.Loading;
            
            var location = await this.locationService.GetLocationAsync();
            if (location is null)
            {
                State = State.PermissionError;
                return;
            }
            
            var forecast = await weatherForecastService.GetForecast(location.Latitude, location.Longitude, apiKey);

            if (forecast?.Main is null)
            {
                State = State.Error;
                return;
            }
        
            Temperature = forecast.Main.Temperature;
            Weather = forecast.Weather.First().Main;
            IconUrl = forecast.Weather.First().IconUrl;
            State = State.Loaded;
        }
        catch (Exception)
        {
            State = State.Error;
        }
    }
    
    private string iconUrl = string.Empty;
    private double temperature;
    private string weather = string.Empty;

    public string IconUrl
    {
        get => iconUrl;
        set => SetProperty(ref iconUrl, value);
    }
    public double Temperature
    {
        get => temperature;
        set => SetProperty(ref temperature, value);
    }
    public string Weather
    {
        get => weather;
        set => SetProperty(ref weather, value);
    }
}