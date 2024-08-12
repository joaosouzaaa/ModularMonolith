using FluentValidation;
using ModularMonolith.Common.Extensions;
using Patient.Domain.Entities;
using Patient.Domain.Enums;

namespace Patient.ApplicationServices.Validators;

public sealed class PatientClientValidator : AbstractValidator<PatientClient>
{
    public PatientClientValidator(IValidator<ContactInfo> contactInfoValidator)
    {
        RuleFor(p => p.ContactInfo).SetValidator(contactInfoValidator);

        RuleFor(p => p.Name)
            .Length(1, 100)
            .NotEmpty()
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name", "1 to 100"));

        RuleFor(p => p.Address)
            .Length(1, 200)
            .NotEmpty()
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Address", "1 to 200"));
    }
}
