using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure.Consumers;

namespace Patient.DependencyInjection;

internal static class ConsumersDependencyInjection
{
    internal static void AddConsumersDependencyInjection(this IServiceCollection services) =>
        services.AddHostedService<AppointmentCreatedConsumer>();
}
