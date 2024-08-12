using ModularMonolith.API.Filters;
using ModularMonolith.Common.Interfaces.Settings;
using ModularMonolith.Common.Settings.NotificationSettings;

namespace ModularMonolith.API.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<NotificationFilter>();

        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
