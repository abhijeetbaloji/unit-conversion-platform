using FluentValidation;
using UnitConversion.Application.DTOs;

namespace UnitConversion.Application.Validators;

public class ConvertRequestValidator
    : AbstractValidator<ConvertRequest>
{
    public ConvertRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotNull();

        RuleFor(x => x.FromUnit)
            .NotEmpty();

        RuleFor(x => x.ToUnit)
            .NotEmpty();
    }
}