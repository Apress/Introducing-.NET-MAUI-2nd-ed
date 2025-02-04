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

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SelectAllText", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetSelectAllOnFocus(true);
#elif IOS || MACCATALYST
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            };
#elif WINDOWS
            handler.PlatformView.GotFocus += (s, e) =>
            {
                handler.PlatformView.SelectAll();
           };
#endif
        });
    }
}