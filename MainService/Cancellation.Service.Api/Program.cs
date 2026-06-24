using Cancellation.Service.Api.Application;
using Cancellation.Service.Api.Infrastructure;
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
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5096"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

