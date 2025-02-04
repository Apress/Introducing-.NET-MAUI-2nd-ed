using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

public partial class ClockWidgetView : Label, IWidgetView
{
    public ClockWidgetView()
    {
        InitializeComponent();
    }

    public IWidgetViewModel WidgetViewModel
    {
        get => (IWidgetViewModel)BindingContext;
        set => BindingContext = value;
    }
}