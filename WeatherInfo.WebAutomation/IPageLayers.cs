using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherInfo.WebAutomation
{
    interface IPageLayers
    {
        void FooterText(IWebDriver driver);
        void HeaderText(IWebDriver driver);
    }
}
