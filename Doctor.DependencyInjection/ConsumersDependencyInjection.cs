using Doctor.ApplicationService.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class ConsumersDependencyInjection
{
    public static void AddConsumersDependencyInjection(this IServiceCollection services)
    {
        services.AddHostedService<AppointmentCreatedConsumer>();
    }
}
