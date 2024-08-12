using Microsoft.Extensions.DependencyInjection;
using Patient.Domain.Interfaces.EmailSettings;
using Patient.Infrastructure.EmailSettings;

namespace Patient.DependencyInjection;

internal static class EmailSettingsDependencyInjection
{
    internal static void AddEmailSettingsDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IEmailSender, EmailSender>();
}
