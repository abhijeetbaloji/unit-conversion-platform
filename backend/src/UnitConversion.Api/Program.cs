using UnitConversion.Application.Interfaces;
using UnitConversion.Application.Services;
using UnitConversion.Domain.Interfaces;
using UnitConversion.Infrastructure.Strategies;
using UnitConversion.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();

builder.Services.AddScoped<IConversionStrategy, LengthConversionStrategy>();

builder.Services.AddScoped<IConversionStrategy, WeightConversionStrategy>();

builder.Services.AddScoped<IConversionStrategy, TemperatureConversionStrategy>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();