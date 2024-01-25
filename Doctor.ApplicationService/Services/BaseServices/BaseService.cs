using FluentValidation;
using ModularMonolith.Common.Interfaces;

namespace Doctor.ApplicationService.Services.BaseServices;
public abstract class BaseService<TEntity>
    where TEntity : class
{
    protected readonly INotificationHandler _notificationHandler;
    private readonly IValidator<TEntity> _validator;
}
