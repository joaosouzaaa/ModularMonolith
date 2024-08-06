using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Extensions;
using FluentValidation;

namespace Doctor.Domain.Validators;

public sealed class DoctorAttendantValidator : AbstractValidator<DoctorAttendant>
{
    public DoctorAttendantValidator()
    {
        RuleFor(d => d.Certification).SetValidator(new CertificationValidator());

        RuleFor(d => d.Name).NotEmpty()
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
