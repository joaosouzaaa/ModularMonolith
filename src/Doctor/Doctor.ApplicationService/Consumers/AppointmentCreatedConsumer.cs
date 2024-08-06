using Doctor.Domain.Constants;
using Doctor.Domain.Contracts;
using Doctor.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ModularMonolith.Common.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Doctor.ApplicationService.Consumers;

public sealed class AppointmentCreatedConsumer(
    IOptions<RabbitMQCredentialsOptions> rabbitMQOptions, 
    IServiceScopeFactory scopeFactory) 
    : BackgroundService
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentialsOptions = rabbitMQOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

        channel.ExchangeDeclare(RabbitMQConstants.AppointmentExchange, ExchangeType.Fanout);

        channel.QueueDeclare(queue: RabbitMQConstants.AppointmentDoctorQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        channel.QueueBind(RabbitMQConstants.AppointmentDoctorQueue, RabbitMQConstants.AppointmentExchange, RabbitMQConstants.AppointmentDoctorQueue);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            await HandleAppointmentTimeCreatedMessageAsync(eventArgs);
        };

        channel.BasicConsume(queue: RabbitMQConstants.AppointmentDoctorQueue, autoAck: true, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            const int millisecondsDelay = 1000;
            await Task.Delay(millisecondsDelay, stoppingToken);
        }
    }

    private async Task HandleAppointmentTimeCreatedMessageAsync(BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var appointmentTimeCreatedEvent = JsonSerializer.Deserialize<AppointmentTimeCreatedEvent>(message)!;

        using var scope = scopeFactory.CreateScope();

        var scheduleService = scope.ServiceProvider.GetRequiredService<IScheduleService>();

        await scheduleService.AddAsync(appointmentTimeCreatedEvent);
    }
}
