using Microsoft.Maui.Controls;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

public partial class SketchWidgetView : GraphicsView, IWidgetView, IDrawable
{
    private DrawingPath? currentPath;
    private readonly IList<DrawingPath> paths = new List<DrawingPath>();

    public SketchWidgetView()
    {
        InitializeComponent();
        this.Drawable = this;
    }

    public IWidgetViewModel WidgetViewModel
    {
        get => (IWidgetViewModel)BindingContext;
        set => BindingContext = value;
    }

    private void OnGraphicsViewStartInteraction(object sender, TouchEventArgs e)
    {
        currentPath = new DrawingPath(Colors.Black, 2);
        currentPath.Add(e.Touches.First());
        paths.Add(currentPath);
        Invalidate();
    }

    private void OnGraphicsViewDragInteraction(object sender, TouchEventArgs e)
    {
        if (currentPath is null)
        {
            return;
        }
        
        currentPath.Add(e.Touches.First());
        Invalidate();
    }

    private void OnGraphicsViewEndInteraction(object sender, TouchEventArgs e)
    {
        if (currentPath is null)
        {
            return;
        }
        currentPath.Add(e.Touches.First());
        Invalidate();

    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (var path in paths)
        {
            canvas.StrokeColor = path.Color;
            canvas.StrokeSize = path.Thickness;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.DrawPath(path.Path);
        }
    }
}