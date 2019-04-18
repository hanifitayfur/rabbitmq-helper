using System.Text;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQService.Abstract;

namespace RabbitMQService
{
    public class RabbitPublisher : IRabbitPublisher
    {
        private readonly IRabbitConnection _rabbitConnection;

        public RabbitPublisher()
        {
            this._rabbitConnection = new RabbitConnection();
        }

        public RabbitPublisher(IRabbitConnection connection)
        {
            _rabbitConnection = connection;
        }

        public void PublishQueen<T>(T data) where T : QueenModel, new()
        {
            PublishQueen(Queens.QueenTopic, data);
        }

        public void PublishQueen<T>(string routingKey, T data) where T : QueenModel, new()
        {
            using (var connection = _rabbitConnection.ConnectionFactory())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: routingKey,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonConvert.SerializeObject(data);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: routingKey,
                                         basicProperties: null,
                                         body: body);
                }

            }
        }
    }
}
