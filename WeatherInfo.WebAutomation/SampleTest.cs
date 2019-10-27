using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WeatherInfo.WebAutomation
{
    public class SampleTest
    {
        static IWebDriver driver;
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void LandingPage()
        {
            driver.Url = "https://www.google.com";
        }
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
