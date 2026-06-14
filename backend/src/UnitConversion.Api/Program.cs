using UnitConversion.Api.Extensions;
using UnitConversion.Api.Middleware;
using UnitConversion.Application.Interfaces;
using UnitConversion.Application.Services;
using UnitConversion.Domain.Interfaces;
using UnitConversion.Infrastructure.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();

builder.Services.AddScoped<IConversionStrategy, LengthConversionStrategy>();
builder.Services.AddScoped<IConversionStrategy, WeightConversionStrategy>();
builder.Services.AddScoped<IConversionStrategy, TemperatureConversionStrategy>();

builder.Services.AddHealthChecks()
    .AddCheck<UnitConversion.Api.Health.HealthCheck>("api");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("Frontend");

app.UseHttpsRedirection();

app.MapControllers();

app.UseHealthEndpoints();

app.Run();