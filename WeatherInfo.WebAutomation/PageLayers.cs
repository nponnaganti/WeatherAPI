using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WeatherInfo.WebAutomation
{
    public class PageLayers : IPageLayers
    {
        public void FooterText(IWebDriver driver)
        {
            var footer = driver.FindElement(By.XPath("/html/body/div[2]/footer/p"));
            var actualFooterText = footer.Text;
            string expectedFooterText = Constants.Footer.footerText;
            Assert.AreEqual(actualFooterText, expectedFooterText);
        }

        public void HeaderText(IWebDriver driver)
        {
            var header = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a"));
            var actualFooterText = header.Text;
            string expectedFooterText = Constants.Header.headerText;
            Assert.AreEqual(actualFooterText, expectedFooterText);
        }
    }
}
