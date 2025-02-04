using System.Text.Json;

namespace WidgetBoard.Communications;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient httpClient;
    private const string ServerUrl = "https://api.openweathermap.org/data/2.5/weather?";

    public WeatherForecastService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Forecast?> GetForecast(double latitude, double longitude, string apiKey)
    {
        var response = await httpClient
            .GetAsync($"{ServerUrl}lat={latitude}&lon={longitude}&units=metric&appid={apiKey}")
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var stringContent = await response.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<Forecast>(stringContent, ForecastContext.Default.Forecast);
    }
}