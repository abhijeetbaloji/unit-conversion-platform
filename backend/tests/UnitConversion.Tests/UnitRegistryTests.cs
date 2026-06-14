using UnitConversion.Domain.Enums;
using UnitConversion.Infrastructure.Registries;

namespace UnitConversion.Tests;

public class UnitRegistryTests
{
    private readonly UnitRegistry _sut = new();

    [Theory]
    [InlineData("meter")]
    [InlineData("kilometer")]
    [InlineData("foot")]
    [InlineData("inch")]
    [InlineData("kilogram")]
    [InlineData("gram")]
    [InlineData("pound")]
    [InlineData("ounce")]
    [InlineData("celsius")]
    [InlineData("fahrenheit")]
    [InlineData("kelvin")]
    public void IsSupported_KnownUnit_ReturnsTrue(string unit)
    {
        Assert.True(_sut.IsSupported(unit));
    }

    [Theory]
    [InlineData("METER")]
    [InlineData("Celsius")]
    public void IsSupported_CaseInsensitive_ReturnsTrue(string unit)
    {
        Assert.True(_sut.IsSupported(unit));
    }

    [Fact]
    public void IsSupported_UnknownUnit_ReturnsFalse()
    {
        Assert.False(_sut.IsSupported("lightyear"));
    }

    [Fact]
    public void GetCategory_LengthUnit_ReturnsLength()
    {
        Assert.Equal(ConversionCategory.Length, _sut.GetCategory("meter"));
    }

    [Fact]
    public void GetCategory_WeightUnit_ReturnsWeight()
    {
        Assert.Equal(ConversionCategory.Weight, _sut.GetCategory("kilogram"));
    }

    [Fact]
    public void GetCategory_TemperatureUnit_ReturnsTemperature()
    {
        Assert.Equal(ConversionCategory.Temperature, _sut.GetCategory("celsius"));
    }

    [Fact]
    public void GetCategory_UnknownUnit_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _sut.GetCategory("lightyear"));
    }
}
