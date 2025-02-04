using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Mac;
using OpenQA.Selenium.Appium.Windows;

namespace WidgetBoard.AutomationTests;

[SetUpFixture]
public class AppiumSetup
{
    private static AppiumDriver? driver;

    public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");
    
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        AppiumServerHelper.StartAppiumLocalServer();

        driver = CreateDriver();
    }

    private static AppiumDriver? CreateDriver()
    {
        var platformName = TestContext.Parameters["platformName"];

        switch (platformName)
        {
            case "Android":
                var androidOptions = new AppiumOptions
                {
                    AutomationName = "UIAutomator2",
                    PlatformName = "Android",
                    App = TestContext.Parameters["app"]
                };

                return new AndroidDriver(androidOptions);
            
            case "iOS":
                var iOSOptions = new AppiumOptions
                {
                    AutomationName = "XCUITest",
                    PlatformName = platformName,
                    PlatformVersion = TestContext.Parameters["platformVersion"] ?? "17.5",
                    DeviceName = TestContext.Parameters["deviceName"] ?? "iPhone 15 Pro",
                    App = TestContext.Parameters["app"]
                };

                return new IOSDriver(iOSOptions);
            
            case "Mac":
                var macOSOptions = new AppiumOptions
                {
                    AutomationName = "mac2",
                    PlatformName = platformName,
                    App = TestContext.Parameters["app"]
                };

                return new MacDriver(macOSOptions);
            
            case "Windows":
                var windowsOptions = new AppiumOptions
                {
                    AutomationName = "windows",
                    PlatformName = "Windows",
                    // The identifier of the deployed application to test
                    App = "com.companyname.widgetboard_9zz4h110yvjzm!App",
                };

                return new WindowsDriver(windowsOptions);
        }

        return null;
    }

    [OneTimeTearDown]
    public void RunAfterAllTests()
    {
        driver?.Quit();

        // If an Appium server was started locally above, make sure we clean it up here
        AppiumServerHelper.DisposeAppiumLocalServer();
    }
}