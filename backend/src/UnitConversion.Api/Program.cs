using UnitConversion.Application.Interfaces;
using UnitConversion.Application.Services;
using UnitConversion.Domain.Interfaces;
using UnitConversion.Infrastructure.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();

builder.Services.AddScoped<IConversionStrategy, LengthConversionStrategy>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();