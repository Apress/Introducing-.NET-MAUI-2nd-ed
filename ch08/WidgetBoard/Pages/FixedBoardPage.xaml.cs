using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public class FixedBoardPage : ContentPage
{
    public FixedBoardPage(FixedBoardPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
