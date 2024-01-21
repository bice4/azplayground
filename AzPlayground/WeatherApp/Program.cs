using Microsoft.AspNetCore.Mvc;
using WeatherApp.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<OpenWeatherHttpClient>();
builder.Services.AddHttpClient<OpenWeatherHttpClient>(c =>
{
    var url = builder.Configuration["OpenWeatherUrl"];
    c.BaseAddress = new Uri(url!);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/cityweather/{city}", async (string city, OpenWeatherHttpClient client) =>
    {
        var weather = await client.GetWeatherByCityNameAsync(city);

        if (weather.Exception != null)
        {
            return Results.Problem("Something went wrong");
        }

        return weather.ErrorMessage != null
            ? Results.Problem(weather.ErrorMessage)
            : Results.Ok(weather.Weather);
    })
    .WithName("GetCityWeather")
    .WithOpenApi();

app.MapGet("/weather", async (
        [FromQuery(Name = "lon")] float lon,
        [FromQuery(Name = "lat")] float lat,
        OpenWeatherHttpClient client) =>
    {
        var weather = await client.GetWeatherByLocationAsync(lon, lat);

        if (weather.Exception != null)
        {
            return Results.Problem("Something went wrong");
        }

        return weather.ErrorMessage != null
            ? Results.Problem(weather.ErrorMessage)
            : Results.Ok(weather.Weather);
    })
    .WithName("GetLocationWeather")
    .WithOpenApi();

app.Run();