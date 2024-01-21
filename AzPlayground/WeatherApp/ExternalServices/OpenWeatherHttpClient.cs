using System.Net;
using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.ExternalServices;

public class OpenWeatherHttpClient(HttpClient httpClient, ILogger<OpenWeatherHttpClient>? logger)
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly ILogger<OpenWeatherHttpClient> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<WeatherResult> GetWeatherByCityNameAsync(string city)
    {
        var apiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");

        try
        {
            var response = await _httpClient.GetAsync($"weather?q={city}&appid={apiKey}&units=metric");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting weather for {City}: {ResponseStatusCode}", city, response.StatusCode);

                return WeatherResult.Failure(response.StatusCode == HttpStatusCode.NotFound
                    ? $"City: {city} not found"
                    : $"Error getting weather for {city}");
            }

            var content = await response.Content.ReadAsStringAsync();

            _logger.LogDebug("Response content: {ResponseContent}", content);

            var weather = JsonSerializer.Deserialize<OpenWeatherModel>(content);

            return weather is null
                ? WeatherResult.Failure("Error deserializing weather")
                : WeatherResult.Success(weather);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occured while getting weather for {City}", city);
            return WeatherResult.Failure(e);
        }
    }

    public async Task<WeatherResult> GetWeatherByLocationAsync(float lon, float lat)
    {
        var apiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");

        try
        {
            var response = await _httpClient.GetAsync($"weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting weather for {Lon} {Lat}: {ResponseStatusCode}", lon, lat,
                    response.StatusCode);

                return WeatherResult.Failure("Error getting weather");
            }

            var content = await response.Content.ReadAsStringAsync();

            _logger.LogDebug("Response content: {ResponseContent}", content);

            var weather = JsonSerializer.Deserialize<OpenWeatherModel>(content);

            return weather is null
                ? WeatherResult.Failure("Error deserializing weather")
                : WeatherResult.Success(weather);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occured while getting weather");
            return WeatherResult.Failure(e);
        }
    }
}