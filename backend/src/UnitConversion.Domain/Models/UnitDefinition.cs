using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Models;

public class UnitDefinition
{
    public string Name { get; init; } = string.Empty;

    public string Symbol { get; init; } = string.Empty;

    public ConversionCategory Category { get; init; }
}
