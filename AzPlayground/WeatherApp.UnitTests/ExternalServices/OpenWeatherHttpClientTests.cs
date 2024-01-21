using System.Net;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WeatherApp.ExternalServices;

namespace WeatherApp.UnitTests.ExternalServices;

[TestFixture]
public class OpenWeatherHttpClientTests
{
    private ILogger<OpenWeatherHttpClient>? _logger;

    private const string RESPONSE =
        """{"coord":{"lon":-0.1257,"lat":51.5085},"weather":[{"id":800,"main":"Clear","description":"clear sky","icon":"01n"}],"base":"stations","main":{"temp":-0.73,"feels_like":-3.03,"temp_min":-2.78,"temp_max":1.16,"pressure":1012,"humidity":81},"visibility":10000,"wind":{"speed":1.79,"deg":327,"gust":3.58},"clouds":{"all":0},"dt":1705604040,"sys":{"type":2,"id":2075535,"country":"GB","sunrise":1705564635,"sunset":1705595024},"timezone":0,"id":2643743,"name":"London","cod":200}""";

    [SetUp]
    public void Setup()
    {
        _logger = Substitute.For<ILogger<OpenWeatherHttpClient>>();
    }

    [Test]
    public void Ctor_Should_Pass()
    {
        // Arrange
        var httpClient = Substitute.For<HttpClient>();

        // Act & Assert
        Assert.DoesNotThrow(() => { _ = new OpenWeatherHttpClient(httpClient, _logger); });
    }

    [Test]
    public void Ctor_Should_Throw_ArgumentNullException_When_HttpClient_Is_Null()
    {
        // Arrange
        HttpClient? httpClient = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => { _ = new OpenWeatherHttpClient(httpClient!, _logger); });
    }

    [Test]
    public void Ctor_Should_Throw_ArgumentNullException_When_Logger_Is_Null()
    {
        // Arrange
        ILogger<OpenWeatherHttpClient>? logger = null;
        var httpClient = Substitute.For<HttpClient>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => { _ = new OpenWeatherHttpClient(httpClient, logger!); });
    }

    [Test]
    public async Task
        GetWeatherByCityNameAsync_Should_Return_ErrorResult_When_Response_Is_Not_Successful_And_HttpStatusCode_Equals_To_NotFound()
    {
        // Arrange
        var expectedErrorMessage = "City: city not found";

        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.NotFound)) {
            BaseAddress = new Uri("http://localhost/")
        };
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByCityNameAsync("city");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(result.Exception, Is.Null);
            Assert.That(result.IsSuccessful, Is.False);
        });
        _logger!.Received().Log(Arg.Is(LogLevel.Error),
            Arg.Is<EventId>(0),
            Arg.Is<object>(x => x.ToString()!.Contains("Error getting weather for")),
            Arg.Is<Exception>(x => x == null),
            Arg.Any<Func<object, Exception, string>>()!);
    }

    [Test]
    public async Task GetWeatherByCityNameAsync_Should_Return_ErrorResult_When_Response_Is_Not_Successful()
    {
        // Arrange
        var expectedErrorMessage = "Error getting weather for city";

        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.BadRequest)) {
            BaseAddress = new Uri("http://localhost/")
        };
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByCityNameAsync("city");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(result.Exception, Is.Null);
            Assert.That(result.IsSuccessful, Is.False);
        });
        _logger!.Received().Log(Arg.Is(LogLevel.Error),
            Arg.Is<EventId>(0),
            Arg.Is<object>(x => x.ToString()!.Contains("Error getting weather for")),
            Arg.Is<Exception>(x => x == null),
            Arg.Any<Func<object, Exception, string>>()!);
    }

    [Test]
    public async Task GetWeatherByCityNameAsync_Should_Return_ErrorResult_When_Exception_Is_Raised()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.BadGateway, true));
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByCityNameAsync("city");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Exception, Is.Not.Null);
            Assert.That(result.IsSuccessful, Is.False);
        });
        _logger!.Received().Log(Arg.Is(LogLevel.Error),
            Arg.Is<EventId>(0),
            Arg.Is<object>(x => x.ToString()!.Contains("Exception occured while getting weather for")),
            Arg.Is<Exception>(x => x != null),
            Arg.Any<Func<object, Exception, string>>()!);
    }

    [Test]
    public async Task GetWeatherByCityNameAsync_Should_Return_SuccessResult_When_Response_Is_Successful()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler(RESPONSE, HttpStatusCode.OK)) {
            BaseAddress = new Uri("http://localhost/")
        };
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByCityNameAsync("city");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Exception, Is.Null);
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.Weather, Is.Not.Null);
        });
    }

    [Test]
    public async Task GetWeatherByLocationAsync_Should_Return_ErrorResult_When_Response_Is_Not_Successful()
    {
        // Arrange
        var expectedErrorMessage = "Error getting weather";

        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.BadRequest)) {
            BaseAddress = new Uri("http://localhost/")
        };
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByLocationAsync(0, 0);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(result.Exception, Is.Null);
            Assert.That(result.IsSuccessful, Is.False);
        });
        _logger!.Received().Log(Arg.Is(LogLevel.Error),
            Arg.Is<EventId>(0),
            Arg.Is<object>(x => x.ToString()!.Contains("Error getting weather for")),
            Arg.Is<Exception>(x => x == null),
            Arg.Any<Func<object, Exception, string>>()!);
    }

    [Test]
    public async Task GetWeatherByLocationAsync_Should_Return_ErrorResult_When_Exception_Is_Raised()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.BadGateway, true));
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByLocationAsync(0,0);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Exception, Is.Not.Null);
            Assert.That(result.IsSuccessful, Is.False);
        });
        _logger!.Received().Log(Arg.Is(LogLevel.Error),
            Arg.Is<EventId>(0),
            Arg.Is<object>(x => x.ToString()!.Contains("Exception occured while getting weather")),
            Arg.Is<Exception>(x => x != null),
            Arg.Any<Func<object, Exception, string>>()!);
    }

    [Test]
    public async Task GetWeatherByLocationAsync_Should_Return_SuccessResult_When_Response_Is_Successful()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler(RESPONSE, HttpStatusCode.OK)) {
            BaseAddress = new Uri("http://localhost/")
        };
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherByLocationAsync(0,0);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.ErrorMessage, Is.Null);
            Assert.That(result.Exception, Is.Null);
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.Weather, Is.Not.Null);
        });
    }
}