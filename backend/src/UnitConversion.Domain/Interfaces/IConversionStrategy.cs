using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Interfaces;

public interface IConversionStrategy
{
    ConversionCategory Category { get; }

    double Convert(
        double value,
        string fromUnit,
        string toUnit);
}