using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherInfo.Models
{
    public class Metric
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Temperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    //WeatherObject Generated using http://json2csharp.com/ from JSON result
    public class WeatherObject
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }
}