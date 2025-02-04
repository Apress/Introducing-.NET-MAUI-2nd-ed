using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WidgetBoard.AutomationTests;

public class BoardTests
{
    private AppiumDriver App => AppiumSetup.App;

    // This could also be an extension method to AppiumDriver if you prefer
    private AppiumElement FindUIElement(string id)
    {
        if (App is WindowsDriver)
        {
            return App.FindElement(MobileBy.AccessibilityId(id));
        }
        
        return App.FindElement(MobileBy.Id(id));
    }
    
    [Test, Order(1)]
    public void SaveButtonIsDisabledByDefault()
    {
        FindUIElement("AddBoardButton").Click();

        var saveButton = FindUIElement("SaveButton");
        
        Assert.That(saveButton.Enabled, Is.False);
        
        FindUIElement("Cancel").Click();
    }
    
    [Order(2)]
    [TestCase("Work", 4, 4)]
    [TestCase("Family", 2, 2)]
    public void CanSaveBoard(string boardName, int numberOfColumns, int numberOfRows)
    {
        FindUIElement("AddBoardButton").Click();

        FindUIElement("BoardNameEntry").SendKeys(boardName);
        FindUIElement("NumberOfColumnsEntry").SendKeys(numberOfColumns.ToString());
        FindUIElement("NumberOfRowsEntry").SendKeys(numberOfRows.ToString());
        
        var saveButton = FindUIElement("SaveButton");
        
        Assert.That(saveButton.Enabled, Is.True);
        saveButton.Click();
        
        var createdBoard = FindUIElement(boardName);
        
        Assert.That(createdBoard, Is.Not.Null);
        Assert.That(createdBoard.Displayed, Is.True);
    }
    
    [Test, Order(3)]
    public void CanSelectEntryInListOfBoards()
    {
        App.FindElement(By.XPath("//XCUIElementTypeStaticText[@name='Work']")).Click();

        var grid = FindUIElement("BoardGrid");
        
        Assert.That(grid, Is.Not.Null);
        Assert.That(grid.Displayed, Is.True);
    }
}