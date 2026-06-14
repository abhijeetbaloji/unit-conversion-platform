using Microsoft.AspNetCore.Mvc;
using UnitConversion.Api.Controllers;
using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;

namespace UnitConversion.Tests;

// Minimal stub — no Moq needed, just implement the interface directly
internal class StubConversionService : IUnitConversionService
{
    private readonly ConvertResponse _response;
    private readonly Exception? _exception;

    public StubConversionService(ConvertResponse response) => _response = response;
    public StubConversionService(Exception exception) => _exception = exception;

    public ConvertResponse Convert(ConvertRequest request)
    {
        if (_exception is not null) throw _exception;
        return _response;
    }
}

public class ConversionControllerTests
{
    // ── POST /api/conversions ─────────────────────────────────────────────

    [Fact]
    public void Convert_ValidRequest_Returns200WithResult()
    {
        var expected = new ConvertResponse
        {
            OriginalValue  = 1,
            FromUnit       = "meter",
            ToUnit         = "kilometer",
            ConvertedValue = 0.001
        };
        var controller = new ConversionController(new StubConversionService(expected));

        var result = controller.Convert(new ConvertRequest
        {
            Value    = 1,
            FromUnit = "meter",
            ToUnit   = "kilometer"
        });

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var body = Assert.IsType<ConvertResponse>(ok.Value);
        Assert.Equal(0.001, body.ConvertedValue);
    }

    [Fact]
    public void Convert_ServiceThrowsArgumentException_BubblesUp()
    {
        var controller = new ConversionController(
            new StubConversionService(new ArgumentException("Unsupported unit: banana")));

        Assert.Throws<ArgumentException>(() =>
            controller.Convert(new ConvertRequest
            {
                Value    = 1,
                FromUnit = "banana",
                ToUnit   = "meter"
            }));
    }

    // ── GET /api/conversions/supported-units ──────────────────────────────

    [Fact]
    public void GetSupportedUnits_Returns200()
    {
        var controller = new ConversionController(
            new StubConversionService(new ConvertResponse()));

        var result = controller.GetSupportedUnits();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetSupportedUnits_ResponseContainsAllThreeCategories()
    {
        var controller = new ConversionController(
            new StubConversionService(new ConvertResponse()));

        var ok   = Assert.IsType<OkObjectResult>(controller.GetSupportedUnits());
        var json = System.Text.Json.JsonSerializer.Serialize(ok.Value);

        Assert.Contains("Length",      json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Weight",      json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Temperature", json, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GetSupportedUnits_LengthContainsMeterAndKilometer()
    {
        var controller = new ConversionController(
            new StubConversionService(new ConvertResponse()));

        var ok   = Assert.IsType<OkObjectResult>(controller.GetSupportedUnits());
        var json = System.Text.Json.JsonSerializer.Serialize(ok.Value);

        Assert.Contains("meter",     json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("kilometer", json, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GetSupportedUnits_TemperatureContainsAllScales()
    {
        var controller = new ConversionController(
            new StubConversionService(new ConvertResponse()));

        var ok   = Assert.IsType<OkObjectResult>(controller.GetSupportedUnits());
        var json = System.Text.Json.JsonSerializer.Serialize(ok.Value);

        Assert.Contains("celsius",    json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("fahrenheit", json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("kelvin",     json, StringComparison.OrdinalIgnoreCase);
    }
}
