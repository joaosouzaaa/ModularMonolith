using Appointment.Domain.Entities;
using Appointment.Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;
public static class ValidatorsDependencyInjection
{
    public static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AppointmentTime>, AppointmentTimeValidator>();
    }
}
