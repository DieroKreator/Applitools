using System.Drawing;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Applitools;

public class BaseClass2
{
    public IWebDriver Driver { get; set; }
    public Eyes Eyes { get; set; }
    public Size Resolution720P => new Size(1280, 720);
    public Size Resolution1080P => new Size(1920, 1080);
    public const string AppName = "UltimateQA";
    //This is a static property used to set the batch name for Applitools
    public static BatchInfo MyBatchInfo { get; set; }

    public IJavaScriptExecutor Javascript { get; set; }
    public IWebElement SocialSharingToolbar => Driver.FindElement(
        By.XPath("//*[@class='et_social_sidebar_networks et_social_visible_sidebar " +
                 "et_social_slideright et_social_animated et_social_rectangle et_social_sidebar_flip " +
                 "et_social_sidebar_withcounts et_social_withtotalcount et_social_mobile_on']"));
    [OneTimeSetUp]
    public void OneTimeSetupBeforeEntireTestClass()
    {
        //This will create a new batch in applitools and store your results there
        MyBatchInfo = new BatchInfo("UltimateQA-DifferentResolutions");
    }
    public void GoToPricingPage()
    {
        //This uses Selenium to navigate to a url of the page below
        Driver.Navigate().GoToUrl("https://www.ultimateqa.com/fake-pricing-page/");
    }
    [SetUp]
    public void SetupForEverySingleTestMethod()
    {
        //create a new chrome driver
        Driver = new ChromeDriver();
        //set an implicit wait for Selenium so that if it doesn't find an element, it will keep trying for specified amount of time
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        Eyes = new Eyes
        {
            ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY",
                EnvironmentVariableTarget.User)
        };
        //set the Applitools batch information to your batch object with some name
        Eyes.Batch = MyBatchInfo;

        Eyes.MatchLevel = MatchLevel.Layout;
        //Add a custom property to all of my tests
        Eyes.AddProperty("PageName", "HomePage");
        //Creating an object that can execute Javascript commands in the browser
        Javascript = (IJavaScriptExecutor)Driver;
        GoToHomePage();
    }
    public void GoToHomePage()
    {
        Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
        WaitForPopUp();
    }

    private void WaitForPopUp()
    {
        Thread.Sleep(60000);
    }
}