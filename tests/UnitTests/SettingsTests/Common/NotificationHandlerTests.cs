using Bogus;
using ModularMonolith.Common.Settings.NotificationSettings;

namespace UnitTests.SettingsTests.Common;

public sealed class NotificationHandlerTests
{
    private readonly NotificationHandler _notificationHandler;
    private readonly Randomizer _random;

    public NotificationHandlerTests()
    {
        _notificationHandler = new NotificationHandler();
        _random = new Faker().Random;
    }

    [Fact]
    public void GetNotifications_AddNotifications_ListHasNotifications()
    {
        // A
        const int notificationCount = 2;
        AddNotificationsInRange(notificationCount);

        // A
        var notificationListResult = _notificationHandler.GetNotifications();

        // A
        Assert.Equal(notificationCount, notificationListResult.Count);
    }

    [Fact]
    public void HasNotifications_AddNotification_HasNotificationTrue()
    {
        // A
        _notificationHandler.AddNotification(_random.Word(), _random.Word());

        // A
        var hasNotificationsResult = _notificationHandler.HasNotifications();

        // A
        Assert.True(hasNotificationsResult);
    }

    [Fact]
    public void HasNotifications_HasNotificationFalse()
    {
        var hasNotificationsResult = _notificationHandler.HasNotifications();

        Assert.False(hasNotificationsResult);
    }

    private void AddNotificationsInRange(int range)
    {
        for (var i = 0; i < range; i++)
        {
            _notificationHandler.AddNotification(_random.Word(), _random.Word());
        }
    }
}
