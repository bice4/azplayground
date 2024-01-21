using WeatherApp.Models;

namespace WeatherApp.UnitTests.Models;

[TestFixture]
public class WeatherResultTests
{
    [Test]
    public void Success_CreatesWeatherResultWithIsSuccessfulTrueAndWeatherData()
    {
        // Arrange
        var weatherModel = new OpenWeatherModel();

        // Act
        var result = WeatherResult.Success(weatherModel);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Weather, Is.EqualTo(weatherModel));
            Assert.That(result.Exception, Is.Null);
        });
    }

    [Test]
    public void FailureWithString_CreatesWeatherResultWithIsSuccessfulFalseAndErrorMessage()
    {
        // Arrange
        const string errorMessage = "Failed to retrieve weather data.";

        // Act
        var result = WeatherResult.Failure(errorMessage);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(result.Weather, Is.Null);
            Assert.That(result.Exception, Is.Null);
        });
    }

    [Test]
    public void FailureWithException_CreatesWeatherResultWithIsSuccessfulFalseAndException()
    {
        // Arrange
        var exception = new InvalidOperationException("An error occurred.");

        // Act
        var result = WeatherResult.Failure(exception);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Weather, Is.Null);
            Assert.That(result.Exception, Is.EqualTo(exception));
        });
    }
}