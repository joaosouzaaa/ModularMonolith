using Appointment.Domain.Constants;
using Appointment.Domain.Contracts;
using Appointment.Domain.Interfaces.Publishers;
using Microsoft.Extensions.Options;
using ModularMonolith.Common.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Appointment.Infrastructure.Publishers;

public sealed class AppointmentPublisher(IOptions<RabbitMQCredentialsOptions> rabbitMQCredentialsOptions) : IAppointmentPublisher
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentials = rabbitMQCredentialsOptions.Value;

    public void PublishAppointmentTimeCreatedMessage(AppointmentTimeCreatedEvent appointment)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQCredentials.HostName,
            Port = _rabbitMQCredentials.Port,
            UserName = _rabbitMQCredentials.UserName,
            Password = _rabbitMQCredentials.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(
            exchange: RabbitMQConstants.AppointmentExchange,
            type: ExchangeType.Fanout);

        channel.QueueDeclare(
            queue: RabbitMQConstants.ApointmentDoctorQueue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.QueueDeclare(
            queue: RabbitMQConstants.ApointmentPatientQueue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var appointmentJsonString = JsonSerializer.Serialize(appointment);
        var body = Encoding.UTF8.GetBytes(appointmentJsonString);

        channel.BasicPublish(
            exchange: RabbitMQConstants.AppointmentExchange,
            routingKey: string.Empty,
            basicProperties: null,
            body: body);
    }
}
