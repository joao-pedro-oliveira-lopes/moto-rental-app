using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using MotoRentalApp.Application.Interfaces.Messaging;
using MotoRentalApp.Application.Events;
using Microsoft.Extensions.DependencyInjection;

namespace MotoRentalApp.Infrastructure.Messaging
{
    public class RabbitMqHostedService : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqHostedService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqHostedService(
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ILogger<RabbitMqHostedService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();


            string queueName = _configuration["RabbitMQ:QueueName"];
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMq Hosted Service started");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var vehicle2024EventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer<Vehicle2024Event>>();

                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var eventMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<Vehicle2024Event>(message);

                        await vehicle2024EventConsumer.ConsumeAsync(eventMessage);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing RabbitMQ message");
                    }
                }
            };
            
            string queueName = _configuration["RabbitMQ:QueueName"];
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMq Hosted Service stopping");
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
