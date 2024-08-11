using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using FluentValidation;
using ModularMonolith.Common.Extensions;

namespace Doctor.ApplicationService.Validators;

public sealed class SpecialityValidator : AbstractValidator<Speciality>
{
    public SpecialityValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .Length(1, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name", "1 to 100"));
    }
}
