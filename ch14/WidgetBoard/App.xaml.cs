namespace WidgetBoard;

public partial class App : Application
{
	private readonly AppShell appShell;

	public App(AppShell appShell)
	{
		this.appShell = appShell;
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(this.appShell);
	}
}