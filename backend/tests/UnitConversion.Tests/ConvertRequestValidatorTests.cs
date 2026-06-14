using FluentValidation.TestHelper;
using UnitConversion.Application.DTOs;
using UnitConversion.Application.Validators;

namespace UnitConversion.Tests;

public class ConvertRequestValidatorTests
{
    private readonly ConvertRequestValidator _validator = new();

    [Fact]
    public void Validate_AllFieldsValid_PassesValidation()
    {
        var request = new ConvertRequest
        {
            Value    = 100,
            FromUnit = "meter",
            ToUnit   = "kilometer"
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_EmptyFromUnit_FailsValidation()
    {
        var request = new ConvertRequest
        {
            Value    = 100,
            FromUnit = "",
            ToUnit   = "kilometer"
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.FromUnit);
    }

    [Fact]
    public void Validate_EmptyToUnit_FailsValidation()
    {
        var request = new ConvertRequest
        {
            Value    = 100,
            FromUnit = "meter",
            ToUnit   = ""
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.ToUnit);
    }

    [Fact]
    public void Validate_NegativeValue_PassesValidation()
    {
        // Negative temperatures are valid (e.g. -40°C), so Value has no range constraint
        var request = new ConvertRequest
        {
            Value    = -40,
            FromUnit = "celsius",
            ToUnit   = "fahrenheit"
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
