using UnitConversion.Application.Constants;
using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Registries;

public class UnitRegistry : IUnitRegistry
{
    public bool IsSupported(string unit)
    {
        return UnitCategories.Units.ContainsKey(unit.ToLower());
    }

    public ConversionCategory GetCategory(string unit)
    {
        if (!UnitCategories.Units.TryGetValue(unit.ToLower(), out var category))
            throw new ArgumentException($"Unsupported unit: {unit}");

        return category;
    }
}
