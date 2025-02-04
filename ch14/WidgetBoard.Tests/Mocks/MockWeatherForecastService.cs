using WidgetBoard.Communications;

namespace WidgetBoard.Tests.Mocks;

public class MockWeatherForecastService : IWeatherForecastService
{
    private readonly Forecast? forecast;
    private readonly TimeSpan delay;

    private MockWeatherForecastService(Forecast? forecast, TimeSpan delay)
    {
        this.forecast = forecast;
        this.delay = delay;
    }

    public static IWeatherForecastService ThatReturns(Forecast? forecast, TimeSpan after) =>
        new MockWeatherForecastService(forecast, after);

    public static IWeatherForecastService ThatReturnsNoForecast(TimeSpan after) =>
        new MockWeatherForecastService(null, after);

    public async Task<Forecast?> GetForecast(double latitude, double longitude, string apiKey)
    {
        await Task.Delay(this.delay);
        return forecast;
    }
}