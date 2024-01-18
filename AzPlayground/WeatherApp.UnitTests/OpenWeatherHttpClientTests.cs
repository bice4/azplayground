using System.Net;
using Microsoft.Extensions.Logging;
using WeatherApp.ExternalServices;

namespace WeatherApp.UnitTests;

[TestFixture]
public class OpenWeatherHttpClientTests
{
    private ILogger<OpenWeatherHttpClient>? _logger;

    private const string RESPONSE = """{"coord":{"lon":-0.1257,"lat":51.5085},"weather":[{"id":800,"main":"Clear","description":"clear sky","icon":"01n"}],"base":"stations","main":{"temp":-0.73,"feels_like":-3.03,"temp_min":-2.78,"temp_max":1.16,"pressure":1012,"humidity":81},"visibility":10000,"wind":{"speed":1.79,"deg":327,"gust":3.58},"clouds":{"all":0},"dt":1705604040,"sys":{"type":2,"id":2075535,"country":"GB","sunrise":1705564635,"sunset":1705595024},"timezone":0,"id":2643743,"name":"London","cod":200}""";

    [SetUp]
    public void Setup()
    {
        _logger = NSubstitute.Substitute.For<ILogger<OpenWeatherHttpClient>>();
    }

    [Test]
    public void Ctor_Should_Pass()
    {
        // Arrange
        var httpClient = NSubstitute.Substitute.For<HttpClient>();

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
        var httpClient = NSubstitute.Substitute.For<HttpClient>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => { _ = new OpenWeatherHttpClient(httpClient, logger!); });
    }
    
    [Test]
    public async Task GetWeatherAsync_Should_Return_Null_When_Response_Is_Not_Successful()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.NotFound));
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherAsync("city");

        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task GetWeatherAsync_Should_Return_Null_When_Exception_Is_Raised()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler("{}", HttpStatusCode.NotFound, true));
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherAsync("city");

        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task GetWeatherAsync_Should_Return_OpenWeatherModel_When_Response_Is_Successful()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler(RESPONSE, HttpStatusCode.OK)) {
            BaseAddress = new Uri("http://localhost/")
        };
        var openWeatherHttpClient = new OpenWeatherHttpClient(httpClient, _logger);

        // Act
        var result = await openWeatherHttpClient.GetWeatherAsync("city");

        // Assert
        Assert.That(result, Is.Not.Null);
    }
}