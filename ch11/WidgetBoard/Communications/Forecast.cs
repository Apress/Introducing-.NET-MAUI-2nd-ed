namespace WidgetBoard.Communications;

public class Forecast
{
    public Main? Main { get; set; }
    public Weather[] Weather { get; set; } = [];
}