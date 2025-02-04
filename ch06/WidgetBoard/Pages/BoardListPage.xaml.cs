using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

public partial class BoardListPage : ContentPage
{
    public BoardListPage(BoardListPageViewModel boardListPageViewModel)
    {
        InitializeComponent();
        BindingContext = boardListPageViewModel;
    }
}