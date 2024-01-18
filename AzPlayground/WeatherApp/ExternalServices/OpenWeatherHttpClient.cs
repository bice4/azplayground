using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.ExternalServices;

public class OpenWeatherHttpClient(HttpClient httpClient, ILogger<OpenWeatherHttpClient>? logger)
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly ILogger<OpenWeatherHttpClient> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<OpenWeatherModel?> GetWeatherAsync(string city)
    {
        var apiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");

        try
        {
            var response = await _httpClient.GetAsync($"weather?q={city}&appid={apiKey}&units=metric");
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting weather for {City}: {ResponseStatusCode}", city, response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            
            _logger.LogDebug("Response content: {ResponseContent}", content);
            
            return JsonSerializer.Deserialize<OpenWeatherModel>(content);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occured while getting weather for {City}", city);
            return null;
        }
        
       
    }
}