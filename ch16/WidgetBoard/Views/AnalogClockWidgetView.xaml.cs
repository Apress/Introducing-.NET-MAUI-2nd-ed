using System.ComponentModel;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

public partial class AnalogClockWidgetView : IWidgetView
{
    public AnalogClockWidgetView()
    {
        InitializeComponent();
    }

    private void ClockWidgetViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AnalogClockWidgetViewModel.Time))
        {
            Invalidate();
        }
    }

    public IWidgetViewModel WidgetViewModel
    {
        get => (IWidgetViewModel)BindingContext;
        set
        {
            BindingContext = value;

            if (BindingContext is IDrawable drawable)
            {
                Drawable = drawable;   
            }

            if (BindingContext is INotifyPropertyChanged propertyChanged)
            {
                propertyChanged.PropertyChanged += ClockWidgetViewModelOnPropertyChanged;
            }
        }
    }
}