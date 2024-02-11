using Patient.ApplicationService.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Patient.DependencyInjection;
public static class ConsumersDependencyInjection
{
    public static void AddConsumersDependencyInjection(this IServiceCollection services)
    {
        services.AddHostedService<AppointmentCreatedConsumer>();
    }
}
