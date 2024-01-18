using Microsoft.EntityFrameworkCore;
using ModularMonolith.Database.DatabaseContexts;

namespace ModularMonolith.API.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ModularMonolithDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("LocalConnection"));
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });
    }
}
