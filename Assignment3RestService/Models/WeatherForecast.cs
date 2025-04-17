using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assignment3RestService.Models
{
    // classes used to deserialize the JSON response from the weather API
    public class WeatherForecastRoot
    {
        [JsonProperty("timelines")]
        public Timelines Timelines { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    // used in the Root class
    public class Timelines
    {
        [JsonProperty("daily")]
        public List<Daily> Daily { get; set; }
    }

    // used in the Root class - the original coordinate
    public class Location
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    // used in the Timelines class
    public class Daily
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("values")]
        public WeatherValues Values { get; set; }
    }

    // used in the Daily class
    public class WeatherValues
    {
        public double cloudBaseAvg { get; set; }
        public double cloudBaseMax { get; set; }
        public double cloudBaseMin { get; set; }
        public double temperatureAvg { get; set; }
        public double temperatureMax { get; set; }
        public double temperatureMin { get; set; }
        public double windSpeedAvg { get; set; }
        public double windSpeedMax { get; set; }
        public double windSpeedMin { get; set; }
        public double humidityAvg { get; set; }
        public double humidityMax { get; set; }
        public double humidityMin { get; set; }
        public double precipitationProbabilityAvg { get; set; }
        public double rainAccumulationSum { get; set; }
        public double snowAccumulationSum { get; set; }
        public DateTime sunriseTime { get; set; }
        public DateTime sunsetTime { get; set; }
    }

}