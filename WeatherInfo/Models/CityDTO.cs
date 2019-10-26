using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherInfo.Models
{
    public class Country
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
    }

    public class AdministrativeArea
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
    }

    public class CityObject
    {
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public Country Country { get; set; }
        public AdministrativeArea AdministrativeArea { get; set; }
    }

    public class CountryWeather
    {
        [Required(ErrorMessage = "Please Select country.")]
        public string country { get; set; }
        [Required(ErrorMessage = "Search Location can't be empty")]
        public string q { get; set; }
        public string Language { get; set; }
        [Display(Name = "Details")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Details { get; set; }
    }
}