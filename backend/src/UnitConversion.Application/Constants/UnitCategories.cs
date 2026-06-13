using UnitConversion.Domain.Enums;

namespace UnitConversion.Application.Constants;

public static class UnitCategories
{
    public static readonly Dictionary<string, ConversionCategory> Units = new()
    {
        { "meter", ConversionCategory.Length },
        { "meters", ConversionCategory.Length },
        { "kilometer", ConversionCategory.Length },
        { "kilometers", ConversionCategory.Length },
        { "foot", ConversionCategory.Length },
        { "feet", ConversionCategory.Length },

        { "kilogram", ConversionCategory.Weight },
        { "kilograms", ConversionCategory.Weight },
        { "gram", ConversionCategory.Weight },
        { "grams", ConversionCategory.Weight },
        { "pound", ConversionCategory.Weight },
        { "pounds", ConversionCategory.Weight },
        { "ounce", ConversionCategory.Weight },
        { "ounces", ConversionCategory.Weight }
    };
}