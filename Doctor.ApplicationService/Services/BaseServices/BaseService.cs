using FluentValidation;
using ModularMonolith.Common.Interfaces;

namespace Doctor.ApplicationService.Services.BaseServices;
public abstract class BaseService<TEntity>(INotificationHandler notificationHandler, IValidator<TEntity> validator)
    where TEntity : class
{
    protected readonly INotificationHandler _notificationHandler = notificationHandler;
    private readonly IValidator<TEntity> _validator = validator;

    protected async Task<bool> ValidateAsync(TEntity entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);

        if (validationResult.IsValid)
            return true;

        foreach(var error in validationResult.Errors)
        {
            _notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
