using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherInfo.Models;

namespace WeatherInfo.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public ActionResult Index(string q = "sydney")
        {
            ViewBag.CityList = GetCitiesByQuery(q);
            ViewBag.txtQuery = q;
            return View();
        }
        [HttpPost]
        public ActionResult Index(CountryWeather obj)
        {
            ViewBag.txtQuery = obj.q;
            if (ModelState.IsValid)
            {
                string appId = "2lDZ55b7flAd9dj7pDIFV8v5jWbfAyLW";
                List<SelectListItem> objCitys = GetCitiesByQuery(obj.q);
                ViewBag.CityList = objCitys;
                ViewBag.CityListCount = objCitys.Count();
                int CntVal = Convert.ToInt32(obj.country.Trim());
                string url = string.Format("http://dataservice.accuweather.com/currentconditions/v1/{0}?apikey={1}&language=en-us&details=false", CntVal,appId);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    List<WeatherObject> objweatherInfo = (new JavaScriptSerializer()).Deserialize<List<WeatherObject>>(json);
                    WeatherObject weatherInfo = objweatherInfo[0];
                    //ViewBag.lblCity_Country = objCitys.Where(x => x.Value == obj.country.Trim()).Select(x => x.Text).FirstOrDefault().ToString();
                    //ViewBag.lblLocalDateTime = DateTime.Parse(weatherInfo.LocalObservationDateTime);
                    ViewBag.imgWeatherIcon = string.Format("https://www.accuweather.com/images/weathericons/{0}.svg", weatherInfo.WeatherIcon.ToString().ToLower());

                    ViewBag.lblText = weatherInfo.WeatherText;

                    ViewBag.lblMetric = string.Format("{0}°{1}", Math.Round(weatherInfo.Temperature.Metric.Value, 1), weatherInfo.Temperature.Metric.Unit);
                    ViewBag.lblImperial = string.Format("{0}°{1}", Math.Round(weatherInfo.Temperature.Imperial.Value, 1), weatherInfo.Temperature.Imperial.Unit);

                    ViewBag.lblMobileLink = weatherInfo.MobileLink;
                    ViewBag.lblWebLink = weatherInfo.Link;
                }
            }
            else
            {
                List<SelectListItem> objCitys = GetCitiesByQuery(obj.q);
                ViewBag.CityList = objCitys;
                ViewBag.CityListCount = objCitys.Count();
                //ViewBag.lblCity_Country = objCitys.Where(x => x.Value == obj.country.Trim()).Select(x => x.Text).FirstOrDefault().ToString();
            }
            return View(obj);
        }
        public List<SelectListItem> GetCitiesByQuery(string q)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            
            try
            {
                string appId = "2lDZ55b7flAd9dj7pDIFV8v5jWbfAyLW";
                string url = string.Format("http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}&language=en-us", appId,q);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    List<CityObject> obj = (new JavaScriptSerializer()).Deserialize<List<CityObject>>(json);

                    foreach (var item in obj)
                    {
                        objList.Add(new SelectListItem() { Text = item.AdministrativeArea.LocalizedName + ", "+item.Country.LocalizedName, Value = item.Key });
                    }
                    return objList;
                }
            }
            catch (Exception ex)
            {
            }
            return objList;
        }
    }
}