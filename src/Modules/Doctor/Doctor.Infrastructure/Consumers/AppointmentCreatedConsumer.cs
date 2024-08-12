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

namespace Doctor.Infrastructure.Consumers;

public sealed class AppointmentCreatedConsumer(
    IOptions<RabbitMQCredentialsOptions> rabbitMQCredentialsOptions,
    IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentials = rabbitMQCredentialsOptions.Value;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQCredentials.HostName,
            Port = _rabbitMQCredentials.Port,
            UserName = _rabbitMQCredentials.UserName,
            Password = _rabbitMQCredentials.Password
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(
            exchange: RabbitMQConstants.AppointmentExchange,
            type: ExchangeType.Fanout);

        channel.QueueDeclare(
            queue: RabbitMQConstants.AppointmentDoctorQueue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.QueueBind(
            queue: RabbitMQConstants.AppointmentDoctorQueue,
            exchange: RabbitMQConstants.AppointmentExchange,
            routingKey: RabbitMQConstants.AppointmentDoctorQueue);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, eventArgs) =>
            HandleAppointmentTimeCreatedMessageAsync(eventArgs, stoppingToken);

        channel.BasicConsume(
            queue: RabbitMQConstants.AppointmentDoctorQueue,
            autoAck: true,
            consumer: consumer);

        return Task.CompletedTask;
    }

    private Task HandleAppointmentTimeCreatedMessageAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var appointmentTimeCreatedEvent = JsonSerializer.Deserialize<AppointmentTimeCreatedEvent>(message)!;

        using var scope = scopeFactory.CreateScope();

        var scheduleService = scope.ServiceProvider.GetRequiredService<IScheduleService>();

        return scheduleService.AddAsync(appointmentTimeCreatedEvent, cancellationToken);
    }
}
