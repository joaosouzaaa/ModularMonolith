using Microsoft.Extensions.DependencyInjection;
using Patient.ApplicationServices.Consumers;

namespace Patient.DependencyInjection;

internal static class ConsumersDependencyInjection
{
    public static void AddConsumersDependencyInjection(this IServiceCollection services) =>
        services.AddHostedService<AppointmentCreatedConsumer>();
}
