using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public partial class FixedBoardPage : ContentPage
{
    public FixedBoardPage(FixedBoardPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}