using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModularMonolith.Common.Interfaces;

namespace ModularMonolith.API.Filters;

public sealed class NotificationFilter(INotificationHandler notificationHandler) : ActionFilterAttribute
{
    private readonly INotificationHandler _notificationHandler = notificationHandler;

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (_notificationHandler.HasNotifications())
            context.Result = new BadRequestObjectResult(_notificationHandler.GetNotifications());

        base.OnActionExecuted(context);
    }
}
