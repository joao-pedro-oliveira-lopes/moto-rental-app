using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MotoRentalApp.Application.Interfaces.Messaging;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace MotoRentalApp.Infrastructure.Messaging
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly ILogger<RabbitMqEventPublisher> _logger;

        public RabbitMqEventPublisher(string hostName, string queueName, ILogger<RabbitMqEventPublisher> logger)
        {
            _hostName = hostName ?? throw new ArgumentNullException(nameof(hostName));
            _queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Publish<T>(T @event) where T : class
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            try
            {
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: properties, body: body);
                _logger.LogInformation("Event published to RabbitMQ: {Event}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing event to RabbitMQ");
                throw;
            }

            return Task.CompletedTask;
        }
    }
}