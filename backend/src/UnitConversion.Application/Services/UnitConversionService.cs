using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Interfaces;
using UnitConversion.Application.Constants;

namespace UnitConversion.Application.Services;

public class UnitConversionService : IUnitConversionService
{
    private readonly IEnumerable<IConversionStrategy> _strategies;

    public UnitConversionService(
        IEnumerable<IConversionStrategy> strategies)
    {
        _strategies = strategies;
    }

    public ConvertResponse Convert(ConvertRequest request)
    {
        if (!UnitCategories.Units.TryGetValue(
                request.FromUnit.ToLower(),
                out var category))
        {
            throw new ArgumentException(
                $"Unsupported unit: {request.FromUnit}");
        }

        var strategy =
            _strategies.First(
                s => s.Category == category);

        var result = strategy.Convert(
            request.Value,
            request.FromUnit,
            request.ToUnit);

        return new ConvertResponse
        {
            OriginalValue = request.Value,
            FromUnit = request.FromUnit,
            ToUnit = request.ToUnit,
            ConvertedValue = Math.Round(result, 6)
        };
    }
}