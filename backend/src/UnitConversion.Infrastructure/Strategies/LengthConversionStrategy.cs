using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;

namespace UnitConversion.Infrastructure.Strategies;

public class LengthConversionStrategy : IConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Length;

    private readonly Dictionary<string, double> _units = new()
    {
        { "meter", 1 },
        { "meters", 1 },
        { "kilometer", 1000 },
        { "kilometers", 1000 },
        { "foot", 0.3048 },
        { "feet", 0.3048 },
        { "inch", 0.0254 },
        { "inches", 0.0254 }
    };

    public double Convert(
        double value,
        string fromUnit,
        string toUnit)
    {
        fromUnit = fromUnit.ToLower();
        toUnit = toUnit.ToLower();

        if (!_units.ContainsKey(fromUnit))
            throw new ArgumentException($"Unsupported unit: {fromUnit}");

        if (!_units.ContainsKey(toUnit))
            throw new ArgumentException($"Unsupported unit: {toUnit}");

        var meters = value * _units[fromUnit];

        return meters / _units[toUnit];
    }
}
