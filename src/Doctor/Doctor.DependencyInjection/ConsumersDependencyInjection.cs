using Doctor.Infrastructure.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;

internal static class ConsumersDependencyInjection
{
    internal static void AddConsumersDependencyInjection(this IServiceCollection services) =>
        services.AddHostedService<AppointmentCreatedConsumer>();
}
