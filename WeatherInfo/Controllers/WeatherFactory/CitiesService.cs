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
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            try
            {
                string apiKey = WebConfigurationManager.AppSettings["WeatherAPIKey"].ToString();
                string autocompleteUrl = WebConfigurationManager.AppSettings["WeatherAutocompleteUrl"].ToString();
                //string url = string.Format("http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}&language=en-us", apikey, q);
                string url = string.Format(autocompleteUrl + "?apikey={0}&q={1}&language={2}", apiKey, q, "en-us");
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    List<CityObject> cities = (new JavaScriptSerializer()).Deserialize<List<CityObject>>(json);

                    foreach (var item in cities)
                    {
                        selectListItems.Add(new SelectListItem() { Text = item.AdministrativeArea.LocalizedName + ", " + item.Country.LocalizedName, Value = item.Key });
                    }
                    return selectListItems;
                }
            }
            catch (Exception)
            {
            }
            return selectListItems;
        }

        public List<SelectListItem> GetCitiesByQueryAndLanguage(string q, string language)
        {
            throw new NotImplementedException();
        }
    }
}