using UnitConversion.Domain.Enums;

namespace UnitConversion.Application.Interfaces;

public interface IUnitRegistry
{
    bool IsSupported(string unit);

    ConversionCategory GetCategory(string unit);
}