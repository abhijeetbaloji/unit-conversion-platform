using UnitConversion.Application.DTOs;

namespace UnitConversion.Application.Interfaces;

public interface IUnitConversionService
{
    ConvertResponse Convert(ConvertRequest request);
}