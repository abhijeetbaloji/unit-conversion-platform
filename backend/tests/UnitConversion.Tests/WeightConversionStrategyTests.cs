using UnitConversion.Infrastructure.Strategies;

namespace UnitConversion.Tests;

public class WeightConversionStrategyTests
{
    private readonly WeightConversionStrategy _sut = new();

    [Fact]
    public void Convert_KilogramToGram_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1, "kilogram", "gram");
        Assert.Equal(1000, result, 6);
    }

    [Fact]
    public void Convert_PoundToKilogram_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1, "pound", "kilogram");
        Assert.Equal(0.453592, result, 5);
    }

    [Fact]
    public void Convert_KilogramToOunce_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1, "kilogram", "ounce");
        Assert.Equal(35.274, result, 2);
    }

    [Fact]
    public void Convert_PluralForms_AreSupported()
    {
        var result = _sut.Convert(1, "kilograms", "grams");
        Assert.Equal(1000, result, 6);
    }

    [Fact]
    public void Convert_SameUnit_ReturnsSameValue()
    {
        var result = _sut.Convert(5, "kilogram", "kilogram");
        Assert.Equal(5, result, 6);
    }

    [Fact]
    public void Convert_UnsupportedUnit_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _sut.Convert(1, "stone", "kilogram"));
    }
}
