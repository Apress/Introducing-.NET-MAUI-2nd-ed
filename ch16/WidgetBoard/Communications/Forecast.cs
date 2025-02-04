using System.Text.Json.Serialization;

namespace WidgetBoard.Communications;

[JsonSerializable(typeof(Forecast))]
partial class ForecastContext : JsonSerializerContext
{
}

public class Forecast
{
    public Main? Main { get; set; }
    public Weather[] Weather { get; set; } = [];
}