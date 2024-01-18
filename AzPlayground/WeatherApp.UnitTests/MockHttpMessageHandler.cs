using System.Net;

namespace WeatherApp.UnitTests;

public class MockHttpMessageHandler(string response, HttpStatusCode statusCode, bool riseException = false)
    : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (riseException)
            throw new Exception("Exception occured");

        return Task.FromResult(new HttpResponseMessage {
            StatusCode = statusCode,
            Content = new StringContent(response)
        });
    }
}