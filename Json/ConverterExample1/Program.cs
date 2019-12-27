using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace SystemTextJsonSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new DictionaryTKeyEnumTValueConverter());

            var o = new WeatherForecastWithEnumDictionary
            {
                Date = DateTimeOffset.Now,
                Summary = "summary",
                TemperatureCelsius = 15,
                TemperatureRanges = new Dictionary<SummaryWordsEnum, int>
                {
                    {SummaryWordsEnum.Cold, 0},
                    {SummaryWordsEnum.Hot, 100}
                }
            };

            var serializedObject = JsonSerializer.Serialize(o, serializeOptions);
            Debug.WriteLine($"{serializedObject}");

            var o2 = JsonSerializer.Deserialize<WeatherForecastWithEnumDictionary>(serializedObject, serializeOptions);
            Debug.WriteLine($"{o2.TemperatureCelsius}");
        }
    }
}
