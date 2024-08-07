using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using FluentValidation;
using ModularMonolith.Common.Extensions;

namespace Doctor.Domain.Validators;

public sealed class DoctorAttendantValidator : AbstractValidator<DoctorAttendant>
{
    public DoctorAttendantValidator(IValidator<Certification> certificationValidator)
    {
        RuleFor(d => d.Certification).SetValidator(certificationValidator);

        RuleFor(d => d.Name)
            .NotEmpty()
            .Length(1, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name", "1 to 100"));

        RuleFor(d => d.ExperienceYears)
            .GreaterThan(0)
            .WithMessage(EMessage.Required.Description().FormatTo("Experience Years"));
        
        const int maximumAge = 110;
        RuleFor(d => d.BirthDate)
            .Must(birthDate => birthDate > DateOnly.FromDateTime(DateTime.Now.AddYears(-maximumAge)))
            .WithMessage($"Doctor needs to be younger than {maximumAge} years old");
    }
}
