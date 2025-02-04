using System.Text.Json.Serialization;

namespace WidgetBoard.Communications;

public class Main
{
    [JsonPropertyName("temp")]
    public double Temperature { get; set; }
}