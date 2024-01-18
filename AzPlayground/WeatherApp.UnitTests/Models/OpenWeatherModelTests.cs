using WeatherApp.Models;

namespace WeatherApp.UnitTests.Models;

[TestFixture]
public class OpenWeatherModelTests
{
    [Test]
    public void WeatherInfo_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var openWeatherModel = new OpenWeatherModel();
        var weatherArray = new[] { new OpenWeatherModel.Weather() };

        // Act
        openWeatherModel.WeatherInfo = weatherArray;

        // Assert
        Assert.That(openWeatherModel.WeatherInfo, Is.EqualTo(weatherArray));
    }

    [Test]
    public void Temperature_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var openWeatherModel = new OpenWeatherModel();
        var temperature = new OpenWeatherModel.Main();

        // Act
        openWeatherModel.Temperature = temperature;

        // Assert
        Assert.That(openWeatherModel.Temperature, Is.EqualTo(temperature));
    }

    [Test]
    public void WindInfo_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var openWeatherModel = new OpenWeatherModel();
        var windInfo = new OpenWeatherModel.Wind();

        // Act
        openWeatherModel.WindInfo = windInfo;

        // Assert
        Assert.That(openWeatherModel.WindInfo, Is.EqualTo(windInfo));
    }

    [Test]
    public void DateTime_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var openWeatherModel = new OpenWeatherModel();
        const int dateTimeValue = 123456;

        // Act
        openWeatherModel.DateTime = dateTimeValue;

        // Assert
        Assert.That(openWeatherModel.DateTime, Is.EqualTo(dateTimeValue));
    }

    [Test]
    public void Timezone_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var openWeatherModel = new OpenWeatherModel();
        const int timezoneValue = 3600;

        // Act
        openWeatherModel.Timezone = timezoneValue;

        // Assert
        Assert.That(openWeatherModel.Timezone, Is.EqualTo(timezoneValue));
    }
    
    [Test]
    public void Main_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var weather = new OpenWeatherModel.Weather();
        const string mainValue = "Clear";

        // Act
        weather.main = mainValue;

        // Assert
        Assert.That(weather.main, Is.EqualTo(mainValue));
    }

    [Test]
    public void Description_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var weather = new OpenWeatherModel.Weather();
        const string descriptionValue = "Clear sky";

        // Act
        weather.description = descriptionValue;

        // Assert
        Assert.That(weather.description, Is.EqualTo(descriptionValue));
    }

    [Test]
    public void Icon_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var weather = new OpenWeatherModel.Weather();
        const string iconValue = "01d";

        // Act
        weather.icon = iconValue;

        // Assert
        Assert.That(weather.icon, Is.EqualTo(iconValue));
    }
    
    [Test]
    public void Temp_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var main = new OpenWeatherModel.Main();
        const double tempValue = 25.5;

        // Act
        main.temp = tempValue;

        // Assert
        Assert.That(main.temp, Is.EqualTo(tempValue));
    }

    [Test]
    public void FeelsLike_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var main = new OpenWeatherModel.Main();
        const double feelsLikeValue = 24.0;

        // Act
        main.feels_like = feelsLikeValue;

        // Assert
        Assert.That(main.feels_like, Is.EqualTo(feelsLikeValue));
    }

    [Test]
    public void TempMin_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var main = new OpenWeatherModel.Main();
        const double tempMinValue = 20.0;

        // Act
        main.temp_min = tempMinValue;

        // Assert
        Assert.That(main.temp_min, Is.EqualTo(tempMinValue));
    }

    [Test]
    public void TempMax_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var main = new OpenWeatherModel.Main();
        const double tempMaxValue = 30.0;

        // Act
        main.temp_max = tempMaxValue;

        // Assert
        Assert.That(main.temp_max, Is.EqualTo(tempMaxValue));
    }

    [Test]
    public void Pressure_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var main = new OpenWeatherModel.Main();
        const int pressureValue = 1010;

        // Act
        main.pressure = pressureValue;

        // Assert
        Assert.That(main.pressure, Is.EqualTo(pressureValue));
    }

    [Test]
    public void Humidity_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var main = new OpenWeatherModel.Main();
        const int humidityValue = 70;

        // Act
        main.humidity = humidityValue;

        // Assert
        Assert.That(main.humidity, Is.EqualTo(humidityValue));
    }
    
    [Test]
    public void Speed_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var wind = new OpenWeatherModel.Wind();
        const double speedValue = 5.0;

        // Act
        wind.speed = speedValue;

        // Assert
        Assert.That(wind.speed, Is.EqualTo(speedValue));
    }

    [Test]
    public void Deg_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var wind = new OpenWeatherModel.Wind();
        const int degValue = 180;

        // Act
        wind.deg = degValue;

        // Assert
        Assert.That(wind.deg, Is.EqualTo(degValue));
    }

    [Test]
    public void Gust_Property_ShouldBeSetCorrectly()
    {
        // Arrange
        var wind = new OpenWeatherModel.Wind();
        const double gustValue = 8.0;

        // Act
        wind.gust = gustValue;

        // Assert
        Assert.That(wind.gust, Is.EqualTo(gustValue));
    }
}