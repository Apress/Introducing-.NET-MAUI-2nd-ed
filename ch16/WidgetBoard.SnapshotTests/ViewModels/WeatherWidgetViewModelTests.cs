using WidgetBoard.Tests.Mocks;
using WidgetBoard.ViewModels;

namespace WidgetBoard.SnapshotTests.ViewModels;

[UsesVerify]
public class WeatherWidgetViewModelTests
{
    [Fact]
    public async Task NullLocationResultsInPermissionErrorState()
    {
        var viewModel = new WeatherWidgetViewModel(
            MockWeatherForecastService.ThatReturnsNoForecast(after: TimeSpan.FromSeconds(5)),
            MockSecureStorage.ThatContains("OpenWeatherApiToken", "SomethingSecure"), 
            MockLocationService.ThatReturnsNoLocation(after: TimeSpan.FromSeconds(2)));

        await viewModel.LoadWeatherForecast();

        Assert.Equal(State.PermissionError, viewModel.State);
        Assert.Equal(viewModel.Weather, string.Empty);
    }
    
    [Fact]
    public async Task NullForecastResultsInErrorState()
    {
        var viewModel = new WeatherWidgetViewModel(
            MockWeatherForecastService.ThatReturnsNoForecast(after: TimeSpan.FromSeconds(5)),
            MockSecureStorage.ThatContains("OpenWeatherApiToken", "SomethingSecure"),
            MockLocationService.ThatReturns( new Location(0.0, 0.0), after: TimeSpan.FromSeconds(2)));

        await viewModel.LoadWeatherForecast();

        Assert.Equal(State.Error, viewModel.State);
        Assert.Equal(viewModel.Weather, string.Empty);
    }

    [Fact]
    public async Task ValidForecastResultsInSuccessfulLoad()
    {
        var weatherForecastService =
            MockWeatherForecastService.ThatReturns(
                new Communications.Forecast
                {
                    Main = new Communications.Main
                    {
                        Temperature = 18.0
                    },
                    Weather = 
                    [
                        new Communications.Weather
                        {
                            Icon = "abc.png",
                            Main = "Sunshine"
                        }
                    ]
                },
                after: TimeSpan.FromSeconds(5));

        var locationService = MockLocationService.ThatReturns(
            new Location(0.0, 0.0),
            after: TimeSpan.FromSeconds(2));

        var viewModel = new WeatherWidgetViewModel(
            weatherForecastService,
            MockSecureStorage.ThatContains("OpenWeatherApiToken", "SomethingSecure"),
            locationService);

        await viewModel.LoadWeatherForecast();

        Assert.Equal(State.Loaded, viewModel.State);
        Assert.Equal("Sunshine", viewModel.Weather);
    }
}