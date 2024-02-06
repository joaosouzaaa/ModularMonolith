using Doctor.Domain.Entities;
using Doctor.Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class ValidatorsDependencyInjection
{
    public static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Certification>, CertificationValidator>();
    }
}
