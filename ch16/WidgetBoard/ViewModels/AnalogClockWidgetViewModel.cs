namespace WidgetBoard.ViewModels;

public class AnalogClockWidgetViewModel : BaseViewModel, IWidgetViewModel, IDrawable
{
    public const string DisplayName = "Analog Clock";
    
    private readonly IDispatcher dispatcher;
    private DateTime time;
    
    public DateTime Time
    {
        get => time;
        set => SetProperty(ref time, value);
    }
    
    public int Position { get; set; }
    
    public string Type => "Analog Clock";
    
    public AnalogClockWidgetViewModel(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
        
        SetTime(DateTime.Now);
    }
    
    public void SetTime(DateTime dateTime)
    {
        Time = dateTime;
        this.dispatcher.DispatchDelayed(
            TimeSpan.FromSeconds(1),
            () => SetTime(DateTime.Now));
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        const int smallIncrement = 6;
        const int largeIncrement = 30;

        canvas.StrokeSize = 5;
        canvas.StrokeColor = App.Current?.PlatformAppTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        
        var radius = dirtyRect.Size.Height / 2;

        canvas.Translate(dirtyRect.Center.X, dirtyRect.Center.Y);
        var hourMarkerLength = dirtyRect.Size.Height / 10;

        for (var i = 1; i <= 12; i++)
        {
            canvas.Rotate(largeIncrement);
            canvas.DrawLine(0, -(radius - hourMarkerLength), 0, -radius);
        }
        
        const float minuteIncrement = smallIncrement / 60f;
        var hourAngle = (Time.Hour * largeIncrement) + Time.Minute * minuteIncrement;
        canvas.Rotate(hourAngle);
        canvas.DrawLine(0, -5, 0, -(dirtyRect.Size.Height / 5));
        canvas.Rotate(-hourAngle);
        
        const float secondIncrement = smallIncrement / 60f;
        var minuteAngle = (Time.Minute * smallIncrement) + Time.Second * secondIncrement;
        canvas.Rotate(minuteAngle);
        canvas.DrawLine(0, -5, 0, -(dirtyRect.Size.Height / 3));
        canvas.Rotate(-minuteAngle);

        canvas.StrokeSize = 3;

        var secondAngle = Time.Second * smallIncrement;
        canvas.Rotate(secondAngle);
        canvas.DrawLine(0, -5, 0, -(dirtyRect.Size.Height / 3));
        canvas.Rotate(-secondAngle);
    }
}