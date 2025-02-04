using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Polly;
using WidgetBoard.Communications;
using WidgetBoard.Data;
using WidgetBoard.Pages;
using WidgetBoard.ViewModels;
using WidgetBoard.Views;
using Refit;
using WidgetBoard.Services;

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
				fonts.AddFont("VT323-Regular.ttf", "VT323");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		builder.Services.AddTransient<AppShell>();
		builder.Services.AddTransient<AppShellViewModel>();
		
		builder.Services.AddSingleton<WidgetFactory>();
		builder.Services.AddSingleton<WidgetTemplateSelector>();
		
		builder.Services.AddSingleton<ILocationService, LocationService>();
		
		builder.Services.AddSingleton(FileSystem.Current);
		builder.Services.AddSingleton(Geolocation.Default);
		builder.Services.AddSingleton(Preferences.Default);
		builder.Services.AddSingleton(SecureStorage.Default);
		builder.Services.AddSingleton(SemanticScreenReader.Default);
		
		builder.Services
			.AddHttpClient<WeatherForecastService>()
			.AddStandardResilienceHandler(static options =>
			{
				options.Retry = new HttpRetryStrategyOptions
				{
					BackoffType = DelayBackoffType.Exponential,
					MaxRetryAttempts = 3,
					UseJitter = true,
					Delay = TimeSpan.FromSeconds(2)
				};
			});
		builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
		
		builder.Services.AddTransient<IBoardRepository, LiteDBBoardRepository>();
		//builder.Services.AddTransient<IBoardRepository, SqliteBoardRepository>();
		
		AddPage<BoardDetailsPage, BoardDetailsPageViewModel>(builder.Services, RouteNames.BoardDetails);
		AddPage<BoardListPage, BoardListPageViewModel>(builder.Services, RouteNames.BoardList);
		AddPage<FixedBoardPage, FixedBoardPageViewModel>(builder.Services, RouteNames.FixedBoard);
		AddPage<SettingsPage, SettingsPageViewModel>(builder.Services, RouteNames.Settings);
		
		WidgetFactory.RegisterWidget<AnalogClockWidgetView, AnalogClockWidgetViewModel>(AnalogClockWidgetViewModel.DisplayName);
		builder.Services.AddTransient<AnalogClockWidgetView>();
		builder.Services.AddTransient<AnalogClockWidgetViewModel>();
		
		WidgetFactory.RegisterWidget<ClockWidgetView, ClockWidgetViewModel>("Clock");
		builder.Services.AddTransient<ClockWidgetView>();
		builder.Services.AddTransient<ClockWidgetViewModel>();
		
		WidgetFactory.RegisterWidget<SketchWidgetView, SketchWidgetViewModel>(SketchWidgetViewModel.DisplayName);
		builder.Services.AddTransient<SketchWidgetView>();
		builder.Services.AddTransient<SketchWidgetViewModel>();

		WidgetFactory.RegisterWidget<WeatherWidgetView, WeatherWidgetViewModel>(WeatherWidgetViewModel.DisplayName);
		builder.Services.AddTransient<WeatherWidgetView>();
		builder.Services.AddTransient<WeatherWidgetViewModel>();

		return builder.Build();
	}
	
	private static void AddPage<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPage, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>(
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
