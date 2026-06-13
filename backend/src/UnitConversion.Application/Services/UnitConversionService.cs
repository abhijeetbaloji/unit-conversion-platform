using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;

namespace UnitConversion.Application.Services;

public class UnitConversionService : IUnitConversionService
{
    public ConvertResponse Convert(ConvertRequest request)
    {
        return new ConvertResponse
        {
            OriginalValue = request.Value,
            FromUnit = request.FromUnit,
            ToUnit = request.ToUnit,
            ConvertedValue = request.Value
        };
    }
}