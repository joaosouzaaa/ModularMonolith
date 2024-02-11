using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure.EmailSettings;
using Patient.Infrastructure.Interfaces.EmailSettings;

namespace Patient.DependencyInjection;
public static class EmailSettingsDependencyInjection
{
    public static void AddEmailSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
    }
}
