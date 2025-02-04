namespace WidgetBoard.Controls;

public class Placeholder : Border
{
    public Placeholder()
    {
        Content = new Label
        {
            Text = "Tap to add widget",
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        SemanticProperties.SetDescription(Content, "Tap to add a widget");
        SemanticProperties.SetHint(Content, "Allows you to choose a widget that can be added to the board at this location.");
    }
    
    public int Position { get; set; }
}