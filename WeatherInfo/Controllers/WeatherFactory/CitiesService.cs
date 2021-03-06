﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherInfo.Models;

namespace WeatherInfo.Controllers.WeatherFactory
{
    public class CitiesService : ICitiesService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CitiesService));
        public List<SelectListItem> GetCitiesByQuery(string q)
        {
            var methodName = string.Format("[{0}.{1}]", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            log.Info(methodName + " Has fired.");
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            try
            {
                string apiKey = WebConfigurationManager.AppSettings["WeatherAPIKey"].ToString();
                string autocompleteUrl = WebConfigurationManager.AppSettings["WeatherAutocompleteUrl"].ToString();
                string url = string.Format(autocompleteUrl + "?apikey={0}&q={1}&language={2}", apiKey, q, "en-us");
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    List<CityObject> cities = (new JavaScriptSerializer()).Deserialize<List<CityObject>>(json);

                    foreach (var item in cities)
                    {
                        selectListItems.Add(new SelectListItem() { Text = item.AdministrativeArea.LocalizedName + ", " + item.Country.LocalizedName, Value = item.Key });
                    }
                    log.Info(methodName + " Obsererd locations "+ selectListItems.Count() +" for the search text " +q);
                    log.Info(methodName + " Successfully retured");
                    return selectListItems;
                }
            }
            catch (Exception ex)
            {
                log.Error(methodName + " ", ex);
            }
            return selectListItems;
        }

        public List<SelectListItem> GetCitiesByQueryAndLanguage(string q, string language)
        {
            var methodName = string.Format("[{0}.{1}]", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            log.Info(methodName + " Has fired.");
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            //default language en-us
            language = !string.IsNullOrEmpty(language) ? language : "en-us";

            try
            {
                string apiKey = WebConfigurationManager.AppSettings["WeatherAPIKey"].ToString();
                string autocompleteUrl = WebConfigurationManager.AppSettings["WeatherAutocompleteUrl"].ToString();
                string url = string.Format(autocompleteUrl + "?apikey={0}&q={1}&language={2}", apiKey, q, language);
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    List<CityObject> cities = (new JavaScriptSerializer()).Deserialize<List<CityObject>>(json);

                    foreach (var item in cities)
                    {
                        selectListItems.Add(new SelectListItem() { Text = item.AdministrativeArea.LocalizedName + ", " + item.Country.LocalizedName, Value = item.Key });
                    }
                    log.Info(methodName + " Obsererd locations " + selectListItems.Count() + " for the search text " + q);
                    log.Info(methodName + " Successfully retured");
                    return selectListItems;
                }
            }
            catch (Exception ex)
            {
                log.Error(methodName + " ", ex);
            }
            return selectListItems;
        }
    }
}