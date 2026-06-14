using Microsoft.AspNetCore.Mvc;
using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;

namespace UnitConversion.Api.Controllers;

[ApiController]
[Route("api/conversions")]
public class ConversionController : ControllerBase
{
    private readonly IUnitConversionService _service;

    public ConversionController(
        IUnitConversionService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult<ConvertResponse> Convert(
        ConvertRequest request)
    {
        return Ok(_service.Convert(request));
    }

    [HttpGet("supported-units")]
    public IActionResult GetSupportedUnits()
    {
        return Ok(new
        {
            Length = new[]
            {
                "meter",
                "kilometer",
                "foot",
                "inch"
            },

            Weight = new[]
            {
                "kilogram",
                "gram",
                "pound",
                "ounce"
            },

            Temperature = new[]
            {
                "celsius",
                "fahrenheit",
                "kelvin"
            }
        });
    }
}