using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;

namespace UnitConversion.Infrastructure.Strategies;

public class TemperatureConversionStrategy : IConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Temperature;

    public double Convert(
        double value,
        string fromUnit,
        string toUnit)
    {
        fromUnit = fromUnit.ToLower();
        toUnit = toUnit.ToLower();

        if (fromUnit == toUnit)
            return value;

        // Convert to Celsius first
        double celsius = fromUnit switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32) * 5 / 9,
            "kelvin" => value - 273.15,
            _ => throw new ArgumentException($"Unsupported unit: {fromUnit}")
        };

        // Convert Celsius to target
        return toUnit switch
        {
            "celsius" => celsius,
            "fahrenheit" => celsius * 9 / 5 + 32,
            "kelvin" => celsius + 273.15,
            _ => throw new ArgumentException($"Unsupported unit: {toUnit}")
        };
    }
}