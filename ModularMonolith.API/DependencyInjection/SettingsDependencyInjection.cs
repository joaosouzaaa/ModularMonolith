using ModularMonolith.Common.Interfaces;
using ModularMonolith.Common.Settings.NotificationSettings;

namespace ModularMonolith.API.DependencyInjection;

public static class SettingsDependencyInjection
{
    public static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
