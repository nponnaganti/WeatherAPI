using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherInfo.Controllers.WeatherFactory;
using WeatherInfo.Models;

namespace WeatherInfo.Controllers
{
    public class WeatherController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(WeatherController));
        private ICitiesService _citiesService;

        public WeatherController(ICitiesService citiesService)
        {
             _citiesService = citiesService;
        }
        // GET: Weather
        public ActionResult Index(string q = "sydney")
        {
            var cities = _citiesService.GetCitiesByQuery(q);
            
            if(cities.Count >0)
            {
                ViewBag.CityList = cities;
                return View();
            }
            else
            {
                return Content("Unauthorized access OR Invalid apiKey");
            }
            
        }
        [HttpPost]
        public ActionResult Index(CountryWeather countryWeather)
        {
            var cities = _citiesService.GetCitiesByQuery(countryWeather.q);
            ViewBag.CityList = cities;
            ViewBag.CityListCount = cities.Count();
            ViewBag.HideTable =  false;

            if (ModelState.IsValid)
            {
                string apikey = WebConfigurationManager.AppSettings["WeatherAPIKey"].ToString();
                string currentConditionsUrl = WebConfigurationManager.AppSettings["WeatherCurrentConditionsUrl"].ToString();

                int CntVal = Convert.ToInt32(countryWeather.country.Trim());
                //string url = string.Format("http://dataservice.accuweather.com/currentconditions/v1/{0}?apikey={1}&language=en-us&details=false", CntVal, apikey);
                string url = string.Format(currentConditionsUrl + "{0}?apikey={1}&language={2}&details={3}", CntVal, apikey, "en-us",false);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    List<WeatherObject> objweatherInfo = (new JavaScriptSerializer()).Deserialize<List<WeatherObject>>(json);
                    WeatherObject weatherInfo = objweatherInfo[0];
                    
                    string weatherIconsUrl = WebConfigurationManager.AppSettings["WeatherIconsUrl"].ToString();
                    //ViewBag.lblLocalDateTime = DateTime.Parse(weatherInfo.LocalObservationDateTime);
                    ViewBag.imgWeatherIcon = string.Format(weatherIconsUrl + "{0}.svg", weatherInfo.WeatherIcon.ToString().ToLower());
                    if (!String.IsNullOrEmpty(countryWeather.country))
                    {
                        var selectedLocation = cities.Where(s => s.Value == countryWeather.country.Trim()).Select(x => x.Text);
                        if (selectedLocation.Count() > 0)
                        {
                            ViewBag.lblCity_Country = selectedLocation.FirstOrDefault();
                        }
                        else
                        {
                            ViewBag.HideTable = true;
                        }
                    }
                    ViewBag.lblText = weatherInfo.WeatherText;

                    ViewBag.lblMetric = string.Format("{0}°{1}", Math.Round(weatherInfo.Temperature.Metric.Value, 1), weatherInfo.Temperature.Metric.Unit);
                    ViewBag.lblImperial = string.Format("{0}°{1}", Math.Round(weatherInfo.Temperature.Imperial.Value, 1), weatherInfo.Temperature.Imperial.Unit);

                    ViewBag.lblMobileLink = weatherInfo.MobileLink;
                    ViewBag.lblWebLink = weatherInfo.Link;
                }
            }
            return View(countryWeather);
        }
    }
}