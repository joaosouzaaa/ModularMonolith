using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Patient.Domain.Entities;
using Patient.Domain.Validators;

namespace Patient.DependencyInjection;
public static class ValidatorsDependencyInjection
{
    public static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ContactInfo>, ContactInfoValidator>();
        services.AddScoped<IValidator<PatientClient>, PatientClientValidator>();
    }
}
