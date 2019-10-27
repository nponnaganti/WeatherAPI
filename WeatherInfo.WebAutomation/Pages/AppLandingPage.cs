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
    public class AppLandingPage
    {
        string homePageUrl = ConfigurationManager.AppSettings["homePageUrl"];
        static IWebDriver driver;
        IPageLayers pageLayers = null;
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(homePageUrl);
            pageLayers = new PageLayers();
        }
       
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void LandingPage_Header()
        {
            pageLayers.HeaderText(driver);
        }
        [Test]
        public void LandingPage_Footer()
        {
            pageLayers.FooterText(driver);
        }
    }
}
