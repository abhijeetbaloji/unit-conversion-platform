using UnitConversion.Infrastructure.Strategies;

namespace UnitConversion.Tests;

public class TemperatureConversionStrategyTests
{
    private readonly TemperatureConversionStrategy _sut = new();

    [Fact]
    public void Convert_CelsiusToFahrenheit_ReturnsCorrectValue()
    {
        var result = _sut.Convert(0, "celsius", "fahrenheit");
        Assert.Equal(32, result, 6);
    }

    [Fact]
    public void Convert_FahrenheitToCelsius_ReturnsCorrectValue()
    {
        var result = _sut.Convert(32, "fahrenheit", "celsius");
        Assert.Equal(0, result, 6);
    }

    [Fact]
    public void Convert_CelsiusToKelvin_ReturnsCorrectValue()
    {
        var result = _sut.Convert(0, "celsius", "kelvin");
        Assert.Equal(273.15, result, 6);
    }

    [Fact]
    public void Convert_KelvinToCelsius_ReturnsCorrectValue()
    {
        var result = _sut.Convert(273.15, "kelvin", "celsius");
        Assert.Equal(0, result, 6);
    }

    [Fact]
    public void Convert_SameUnit_ReturnsSameValue()
    {
        var result = _sut.Convert(100, "celsius", "celsius");
        Assert.Equal(100, result, 6);
    }

    [Fact]
    public void Convert_BoilingPoint_CelsiusToFahrenheit()
    {
        var result = _sut.Convert(100, "celsius", "fahrenheit");
        Assert.Equal(212, result, 6);
    }

    [Fact]
    public void Convert_UnsupportedUnit_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _sut.Convert(0, "rankine", "celsius"));
    }
}
