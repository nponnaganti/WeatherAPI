using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherInfo.Models;

namespace WeatherInfo.Controllers.WeatherFactory
{
    public class CitiesService : ICitiesService
    {
        public List<SelectListItem> GetCitiesByQuery(string q)
        {
            List<SelectListItem> objList = new List<SelectListItem>();

            try
            {
                string apikey = WebConfigurationManager.AppSettings["WeatherAPIKey"].ToString();
                string url = string.Format("http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}&language=en-us", apikey, q);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    List<CityObject> obj = (new JavaScriptSerializer()).Deserialize<List<CityObject>>(json);

                    foreach (var item in obj)
                    {
                        objList.Add(new SelectListItem() { Text = item.AdministrativeArea.LocalizedName + ", " + item.Country.LocalizedName, Value = item.Key });
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