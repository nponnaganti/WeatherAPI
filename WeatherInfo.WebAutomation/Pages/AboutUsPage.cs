using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WeatherInfo.WebAutomation.Pages
{
    public class AboutUsPage
    {
        static IWebDriver driver;
        string homePageUrl = ConfigurationManager.AppSettings["homePageUrl"];
        string aboutUs = "Home/About";
        IPageLayers pageLayers = null;

        #region SetUp of AboutUs Page
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(homePageUrl + aboutUs);
            pageLayers = new PageLayers();
        }
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
            driver.Quit();
        }
        #endregion

        [Test]
        public void AboutUsPage_Test()
        {
            driver.Navigate().GoToUrl(homePageUrl + aboutUs);
        }
    }
}
