using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ECPMaster.AsyncDataServices
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exhangeName;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() {Uri = new Uri(_configuration["RabbitMQHost"])};
            _exhangeName = _configuration["ECP_EXCHANGE"];
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: _exhangeName, type: ExchangeType.Direct, durable: true);
                _connection.ConnectionShutdown += RabbitMqConnectionShutdown;
                Console.WriteLine("Connected to RabbitMQ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"could not connect to rabbitmq server {e.Message}");
                throw;
            }
        }

        private void RabbitMqConnectionShutdown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("----> RabbitMQ connection shutdown");
        }

        private void SendMessage(string payload)
        {
            if (_connection.IsOpen)
            {
                var body = Encoding.UTF8.GetBytes(payload);
                _channel.BasicPublish(
                    exchange: _exhangeName,
                    routingKey: "SLAVE_SERVICE_BINDING_KEY_1",
                    basicProperties:null,
                    body: body);
                Console.WriteLine($"message sent by Master Node {payload}");
            }
            else
            {
                Console.WriteLine("RabbitMQ Conenction is closed, can't send message");
            }
        }

        public void Dispose()
        {
            if (!_connection.IsOpen) return;
            _channel.Close();
            _connection.Close();
        }

        public void CreateAJob()
        {
            SendMessage(JsonConvert.SerializeObject(new {EVENT = "START_JOB"}));
        }
    }
}