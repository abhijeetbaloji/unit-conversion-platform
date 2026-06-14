using UnitConversion.Infrastructure.Strategies;

namespace UnitConversion.Tests;

public class LengthConversionStrategyTests
{
    private readonly LengthConversionStrategy _sut = new();

    [Fact]
    public void Convert_MeterToKilometer_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1000, "meter", "kilometer");
        Assert.Equal(1, result, 6);
    }

    [Fact]
    public void Convert_KilometerToMeter_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1, "kilometer", "meter");
        Assert.Equal(1000, result, 6);
    }

    [Fact]
    public void Convert_MeterToFoot_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1, "meter", "foot");
        Assert.Equal(3.28084, result, 4);
    }

    [Fact]
    public void Convert_MeterToInch_ReturnsCorrectValue()
    {
        var result = _sut.Convert(1, "meter", "inch");
        Assert.Equal(39.3701, result, 3);
    }

    [Fact]
    public void Convert_PluralForms_AreSupported()
    {
        var result = _sut.Convert(1000, "meters", "kilometers");
        Assert.Equal(1, result, 6);
    }

    [Fact]
    public void Convert_SameUnit_ReturnsSameValue()
    {
        var result = _sut.Convert(42, "meter", "meter");
        Assert.Equal(42, result, 6);
    }

    [Fact]
    public void Convert_UnsupportedFromUnit_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _sut.Convert(1, "mile", "meter"));
    }

    [Fact]
    public void Convert_UnsupportedToUnit_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _sut.Convert(1, "meter", "mile"));
    }
}
