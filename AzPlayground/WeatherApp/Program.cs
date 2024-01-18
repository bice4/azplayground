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


app.MapGet("/weatherforecast/{city}", async (string city, OpenWeatherHttpClient client) =>
    {
        var weather = await client.GetWeatherAsync(city);

        return weather is null ? Results.NotFound() : Results.Ok(weather);
    })
    .WithName("GetWeather")
    .WithOpenApi();

app.Run();