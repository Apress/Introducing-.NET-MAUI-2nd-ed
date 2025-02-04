using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public partial class BoardDetailsPage : ContentPage
{
    public BoardDetailsPage(BoardDetailsPageViewModel boardDetailsPageViewModel)
    {
        InitializeComponent();
        BindingContext = boardDetailsPageViewModel;
    }
}