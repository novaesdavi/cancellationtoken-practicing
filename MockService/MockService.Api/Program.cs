var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (CancellationToken cancellationToken) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    try
    {
        for (int i = 0; i < 10; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Process was cancelled in the for loop");

            }
            ///OR 
            ///
            //Mesma exceção se for o Task.Delay
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(1000); // Simulate work
            //await Task.Delay(1000, cancellationToken); // Simulate work
        }
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Process was cancelled in the catch block.");
        return Results.Json(new { Message = "Process was cancelled." }, statusCode: 499);
    }

    Console.WriteLine(forecast);
    return Results.Json(forecast);
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
