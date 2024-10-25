
using System.Text;
using System.Text.Json;
using PlatformService.DTOs;
using RabbitMQ.Client;

namespace AsyncDataServices {
    public class MesaageBusClientImpl : IMessageClient
    {

        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public MesaageBusClientImpl(IConfiguration configuration) {
            _configuration = configuration;
            var factory = new RabbitMQ.Client.ConnectionFactory() {
                HostName = _configuration["RabbitMQHOST"],
                Port = int.Parse(_configuration["RabbitMQPort"])
                // UserName = _configuration["EventBusConnection:Username"],
                // Password = _configuration["EventBusConnection:Password"]
            };
            try {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("Connected to RabbitMQ");
            }
            catch(Exception ex) {
                Console.WriteLine($"Error connecting to RabbitMQ: {ex.Message}");
            }
        }
        public void publishNewPlatform(PlatformPublishDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            var body = Encoding.UTF8.GetBytes(message);
            if(_connection.IsOpen) { 
                Console.WriteLine($"Publishing message: {message}");
            } else {
                Console.WriteLine("Cannot publish message, RabbitMQ is not connected.");
                return;
            }
        }

        private void SendMessage(string message) {
            var body = Encoding.UTF8.GetBytes(message);
            // var properties = _channel.CreateBasicProperties();
            // properties.Persistent = true;

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"Sent message: {message}");
        }

        public void Dispose() {
            _channel.Close();
            _connection.Close();
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) {
            Console.WriteLine($"Error connecting to RabbitMQ: {e.ReplyText}");
            // _connection.Close();
        }
    }
}