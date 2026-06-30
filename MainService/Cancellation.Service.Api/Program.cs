using Cancellation.Service.Api.Application;
using Cancellation.Service.Api.Infrastructure;
using Cancellation.Service.Api.Middleware;
using Microsoft.AspNetCore.Http.HttpResults;
using Refit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddScoped<IProcessUseCase, ProcessUseCase>();
builder.Services.AddScoped<IRepositoryMock, RepositoryMock>();

builder.Services
    .AddRefitClient<IMockApi>()
    .ConfigureHttpClient(c => 
    {
        c.BaseAddress = new Uri("http://localhost:5096");
        c.Timeout = TimeSpan.FromSeconds(5); // Set timeout at HttpClient level
    });


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

