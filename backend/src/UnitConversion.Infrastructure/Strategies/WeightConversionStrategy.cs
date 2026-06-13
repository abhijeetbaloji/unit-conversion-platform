using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;

namespace UnitConversion.Infrastructure.Strategies;

public class WeightConversionStrategy : IConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Weight;

    private readonly Dictionary<string, double> _units = new()
    {
        { "kilogram", 1 },
        { "kilograms", 1 },

        { "gram", 0.001 },
        { "grams", 0.001 },

        { "pound", 0.45359237 },
        { "pounds", 0.45359237 },

        { "ounce", 0.0283495 },
        { "ounces", 0.0283495 }
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

        var kilograms = value * _units[fromUnit];

        return kilograms / _units[toUnit];
    }
}