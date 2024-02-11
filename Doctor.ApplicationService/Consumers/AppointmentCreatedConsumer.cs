using Doctor.ApplicationService.Interfaces.Services;
using Doctor.Domain.Constants;
using Doctor.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ModularMonolith.Common.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Doctor.ApplicationService.Consumers;
public sealed class AppointmentCreatedConsumer(IOptions<RabbitMQCredentialsOptions> rabbitMQOptions, IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentialsOptions = rabbitMQOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQCredentialsOptions.HostName,
                Port = _rabbitMQCredentialsOptions.Port,
                UserName = _rabbitMQCredentialsOptions.UserName,
                Password = _rabbitMQCredentialsOptions.Password
            };
            
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: RabbitMQConstants.ApointmentQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += async (sender, eventArgs) =>
            {
                await HandleAppointmentTimeCreatedMessageAsync(eventArgs);
            };

            channel.BasicConsume(queue: RabbitMQConstants.ApointmentQueue, autoAck: true, consumer: consumer);

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task HandleAppointmentTimeCreatedMessageAsync(BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var appointmentTimeCreatedEvent = JsonSerializer.Deserialize<AppointmentTimeCreatedEvent>(message);

        using var scope = scopeFactory.CreateScope();

        var scheduleService = scope.ServiceProvider.GetRequiredService<IScheduleService>();

        await scheduleService.AddAsync(appointmentTimeCreatedEvent!);
    }
}
