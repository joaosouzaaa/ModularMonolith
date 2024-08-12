using ModularMonolith.Common.Interfaces.Settings;

namespace ModularMonolith.Common.Settings.NotificationSettings;

public sealed class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notificationList;

    public NotificationHandler() =>
        _notificationList = [];

    public List<Notification> GetNotifications() =>
        _notificationList;

    public bool HasNotifications() =>
        _notificationList.Any();

    public void AddNotification(string key, string message) =>
        _notificationList.Add(new(key, message));
}
