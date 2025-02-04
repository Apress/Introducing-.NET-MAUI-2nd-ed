using OpenQA.Selenium.Appium.Service;

namespace WidgetBoard.AutomationTests;

public static class AppiumServerHelper
{
    private static AppiumLocalService? appiumLocalService;

    private const string DefaultHostAddress = "127.0.0.1";
    private const int DefaultHostPort = 4723;

    public static void StartAppiumLocalServer(
        string host = DefaultHostAddress,
        int port = DefaultHostPort)
    {
        if (appiumLocalService is not null)
        {
            return;
        }

        var builder = new AppiumServiceBuilder()
            .WithIPAddress(host)
            .UsingPort(port);

        // Start the server with the builder
        appiumLocalService = builder.Build();
        appiumLocalService.Start();
    }

    public static void DisposeAppiumLocalServer()
    {
        appiumLocalService?.Dispose();
    }
}