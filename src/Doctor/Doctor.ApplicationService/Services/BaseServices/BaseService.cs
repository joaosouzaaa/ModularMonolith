using FluentValidation;
using ModularMonolith.Common.Interfaces.Settings;

namespace Doctor.ApplicationService.Services.BaseServices;

public abstract class BaseService<TEntity>
    where TEntity : class
{
    protected readonly INotificationHandler _notificationHandler;
    private readonly IValidator<TEntity> _validator;

    public BaseService(
        INotificationHandler notificationHandler,
        IValidator<TEntity> validator)
    {
        _notificationHandler = notificationHandler;
        _validator = validator;
    }

    protected async Task<bool> IsValidAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var error in validationResult.Errors)
        {
            _notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
