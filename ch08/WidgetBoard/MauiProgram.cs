using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Accessibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using WidgetBoard.Pages;
using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		builder.Services.AddTransient<AppShell>();
		builder.Services.AddTransient<AppShellViewModel>();
		
		builder.Services.AddSingleton<WidgetFactory>();
		builder.Services.AddSingleton<WidgetTemplateSelector>();
		builder.Services.AddSingleton(SemanticScreenReader.Default);
		
		AddPage<BoardDetailsPage, BoardDetailsPageViewModel>(builder.Services, RouteNames.BoardDetails);
		AddPage<BoardListPage, BoardListPageViewModel>(builder.Services, RouteNames.BoardList);
		AddPage<FixedBoardPage, FixedBoardPageViewModel>(builder.Services, RouteNames.FixedBoard);
		AddPage<SettingsPage, SettingsPageViewModel>(builder.Services, RouteNames.Settings);
		
		WidgetFactory.RegisterWidget<ClockWidgetView, ClockWidgetViewModel>("Clock");
		builder.Services.AddTransient<ClockWidgetView>();
		builder.Services.AddTransient<ClockWidgetViewModel>();


		return builder.Build();
	}
	
	private static void AddPage<TPage, TViewModel>(
		IServiceCollection services,
		string route)
		where TPage : Page
		where TViewModel : BaseViewModel
	{
		services
			.AddTransient(typeof(TPage))
			.AddTransient(typeof(TViewModel));
		Routing.RegisterRoute(route, typeof(TPage));
	}
}
