using ModularMonolith.API.Constants;
using ModularMonolith.Common.Options;

namespace ModularMonolith.API.DependencyInjection;
public static class OptionsDependencyInjection
{
    public static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQCredentialsOptions>(options => configuration.GetSection(OptionsConstants.RabbitMQCredentialsSection).Bind(options));
        services.Configure<EmailCredentialsOptions>(options => configuration.GetSection(OptionsConstants.EmailCredentialsSection).Bind(options));
    }
}
