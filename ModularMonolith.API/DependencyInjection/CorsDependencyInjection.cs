using ModularMonolith.API.Constants;

namespace ModularMonolith.API.DependencyInjection;

public static class CorsDependencyInjection
{
    public static void AddCorsDependencyInjection(this IServiceCollection services)
    {
        services.AddCors(p => p.AddPolicy(CorsPoliciesNamesConstants.CorsPolicy, builder =>
        {
            builder.AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true)
                   .AllowCredentials();
        }));
    }
}
