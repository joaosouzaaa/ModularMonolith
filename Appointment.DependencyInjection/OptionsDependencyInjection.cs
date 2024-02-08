using Appointment.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;
public static class OptionsDependencyInjection
{
    public static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        const string rabbitMQCredentialsSection = "RabbitMQCredentials";
        services.Configure<RabbitMQCredentialsOptions>(options => configuration.GetSection(rabbitMQCredentialsSection));
    }
}
