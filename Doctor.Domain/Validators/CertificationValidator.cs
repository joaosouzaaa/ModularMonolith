using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Extensions;
using FluentValidation;

namespace Doctor.Domain.Validators;
public sealed class CertificationValidator : AbstractValidator<Certification>
{
    public CertificationValidator()
    {
        RuleFor(c => c.LicenseNumber).Length(20)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("License Number", "20"));
    }
}
