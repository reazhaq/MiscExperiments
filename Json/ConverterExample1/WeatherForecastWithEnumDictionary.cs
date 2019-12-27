using System;
using System.Collections.Generic;

namespace SystemTextJsonSamples
{
    public class WeatherForecastWithEnumDictionary
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
        public Dictionary<SummaryWordsEnum, int> TemperatureRanges { get; set; }
    }

    public enum SummaryWordsEnum
    {
        Cold, Hot
    }
}