using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UFynd.Application;
using UFynd.Application.Middleware;
using UFynd.Application.Feature.Hotel.Queries;
using UFynd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UFynd API", Version = "v1" });
});

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetHotelRatesQuery).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UFynd API V1");
});

app.MapControllers();
app.UseCustomExceptionHandler();
app.Run();
