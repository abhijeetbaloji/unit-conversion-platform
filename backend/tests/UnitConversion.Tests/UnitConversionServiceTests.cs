using UnitConversion.Application.DTOs;
using UnitConversion.Application.Services;
using UnitConversion.Infrastructure.Strategies;

namespace UnitConversion.Tests;

public class UnitConversionServiceTests
{
    private readonly UnitConversionService _sut;

    public UnitConversionServiceTests()
    {
        var strategies = new[]
        {
            (UnitConversion.Domain.Interfaces.IConversionStrategy)new LengthConversionStrategy(),
            new WeightConversionStrategy(),
            new TemperatureConversionStrategy()
        };
        _sut = new UnitConversionService(strategies);
    }

    [Fact]
    public void Convert_ValidLengthRequest_ReturnsConvertedValue()
    {
        var request = new ConvertRequest
        {
            Value = 1000,
            FromUnit = "meter",
            ToUnit = "kilometer"
        };

        var result = _sut.Convert(request);

        Assert.Equal(1, result.ConvertedValue, 6);
        Assert.Equal("meter", result.FromUnit);
        Assert.Equal("kilometer", result.ToUnit);
        Assert.Equal(1000, result.OriginalValue);
    }

    [Fact]
    public void Convert_ValidWeightRequest_ReturnsConvertedValue()
    {
        var request = new ConvertRequest
        {
            Value = 1,
            FromUnit = "kilogram",
            ToUnit = "gram"
        };

        var result = _sut.Convert(request);

        Assert.Equal(1000, result.ConvertedValue, 6);
    }

    [Fact]
    public void Convert_ValidTemperatureRequest_ReturnsConvertedValue()
    {
        var request = new ConvertRequest
        {
            Value = 100,
            FromUnit = "celsius",
            ToUnit = "fahrenheit"
        };

        var result = _sut.Convert(request);

        Assert.Equal(212, result.ConvertedValue, 6);
    }

    [Fact]
    public void Convert_UnsupportedUnit_ThrowsArgumentException()
    {
        var request = new ConvertRequest
        {
            Value = 1,
            FromUnit = "lightyear",
            ToUnit = "meter"
        };

        Assert.Throws<ArgumentException>(() => _sut.Convert(request));
    }

    [Fact]
    public void Convert_ResultRoundedToSixDecimals()
    {
        var request = new ConvertRequest
        {
            Value = 1,
            FromUnit = "pound",
            ToUnit = "kilogram"
        };

        var result = _sut.Convert(request);

        // result should be rounded to 6 decimal places
        var rounded = Math.Round(result.ConvertedValue, 6);
        Assert.Equal(rounded, result.ConvertedValue);
    }
}
