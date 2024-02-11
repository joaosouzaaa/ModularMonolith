using Appointment.Domain.Contracts;
using Appointment.Infrastructure.Interfaces.Publishers;
using Microsoft.Extensions.Options;
using ModularMonolith.Common.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Appointment.Infrastructure.Publishers;
public sealed class AppointmentPublisher(IOptions<RabbitMQCredentialsOptions> rabbitMQOptions) : IAppointmentPublisher
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentialsOptions = rabbitMQOptions.Value;

    public void PublishAppointmentTimeCreatedMessage(AppointmentTimeCreatedEvent appointment)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQCredentialsOptions.HostName,
            UserName = _rabbitMQCredentialsOptions.UserName,
            Password = _rabbitMQCredentialsOptions.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        const string queueName = "appointment_queue";
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var appointmentJsonString = JsonSerializer.Serialize(appointment);
        var body = Encoding.UTF8.GetBytes(appointmentJsonString);

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
}
