namespace WeatherApp.Models;

public sealed class WeatherResult
{
    public bool IsSuccessful { get; private set; }

    public string? ErrorMessage { get; private set; }

    public OpenWeatherModel? Weather { get; private set; }

    public Exception? Exception { get; private set; }


    public static WeatherResult Success(OpenWeatherModel weather) => new(true, null, weather, null);

    public static WeatherResult Failure(string errorMessage) => new(false, errorMessage, null, null);

    public static WeatherResult Failure(Exception exception) => new(false, null, null, exception);

    private WeatherResult(bool isSuccessful, string? errorMessage, OpenWeatherModel? weather, Exception? exception)
    {
        IsSuccessful = isSuccessful;
        ErrorMessage = errorMessage;
        Weather = weather;
        Exception = exception;
    }
}