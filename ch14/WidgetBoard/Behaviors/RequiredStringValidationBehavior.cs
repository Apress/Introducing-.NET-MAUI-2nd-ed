namespace WidgetBoard.Behaviors;

public class RequiredStringValidationBehavior : Behavior<Entry>
{
    public Style? ValidStyle { get; set; }
    
    public Style? InvalidStyle { get; set; }
    
    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);
        
        bindable.TextChanged += BindableOnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        base.OnDetachingFrom(bindable);
        
        bindable.TextChanged -= BindableOnTextChanged;
    }
    
    private void BindableOnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry &&
            InvalidStyle is not null &&
            ValidStyle is not null)
        {
            entry.Style = string.IsNullOrWhiteSpace(e.NewTextValue) ? InvalidStyle : ValidStyle;
        }
    }
}