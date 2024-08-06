using ModularMonolith.API.Constants;
using ModularMonolith.Common.Options;

namespace ModularMonolith.API.DependencyInjection;
internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQCredentialsOptions>(configuration.GetSection(OptionsConstants.RabbitMQCredentialsSection));
        services.Configure<EmailCredentialsOptions>(configuration.GetSection(OptionsConstants.EmailCredentialsSection));
    }
}
