using ModularMonolith.Common.Options;

namespace ModularMonolith.API.DependencyInjection;
public static class OptionsDependencyInjection
{
    public static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        const string rabbitMQCredentialsSection = "RabbitMQCredentials";
        services.Configure<RabbitMQCredentialsOptions>(options => configuration.GetSection(rabbitMQCredentialsSection));
    }
}
