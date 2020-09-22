using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace iLive_BDT
{
    [Binding]
    public sealed class Hooks1
    {
        private readonly string baseURL = "http://ilive-web.cmsplanet.ru/";
        private RemoteWebDriver driver;
        static public String testName = String.Empty;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver();
            ScenarioContext.Current["driver"] = driver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Navigate().GoToUrl(this.baseURL);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //AutotestSupport.AutotestSupport.TakeScreenshot(driver, testName);
            driver.Quit();
        }
    }
}
