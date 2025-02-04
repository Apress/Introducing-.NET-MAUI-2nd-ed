using Refit;

namespace WidgetBoard.Communications;

public interface IWeatherForecastService
{
    [Get("/weather?lat={latitude}&lon={longitude}&units=metric&appid={apiKey}")]
    Task<Forecast?> GetForecast(double latitude, double longitude, string apiKey);
}