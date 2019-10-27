using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeatherInfo.Models;

namespace WeatherInfo.Controllers.WeatherFactory
{
    public interface ICitiesService
    {
        List<SelectListItem> GetCitiesByQuery(string q);
        List<SelectListItem> GetCitiesByQueryAndLanguage(string q, string language);
        //List<CityObject> GetCitiesByQuery(string q);
    }
}
