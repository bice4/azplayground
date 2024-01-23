using System.Text.Json.Serialization;

namespace WeatherApp.Models;

public class OpenWeatherModel
{
    [JsonPropertyName("weather")]
    public Weather[]? WeatherInfo { get; set; }
    
    [JsonPropertyName("main")]
    public Main? Temperature { get; set; }
    
    [JsonPropertyName("wind")]
    public Wind? WindInfo { get; set; }
    
    [JsonPropertyName("dt")] 
    public int DateTime { get; set; }
    
    [JsonPropertyName("timezone")] 
    public int Timezone { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    public class Weather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }
}