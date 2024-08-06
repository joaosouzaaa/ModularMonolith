using ModularMonolith.Common.Settings.NotificationSettings;

namespace ModularMonolith.Common.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
