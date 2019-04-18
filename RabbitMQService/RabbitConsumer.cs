using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQService.Abstract;

namespace RabbitMQService
{
    public class RabbitConsumer : IRabbitConsumer
    {
        private readonly int TaskTimeout = 30;
        private readonly IRabbitConnection _rabbitConnection;

        public RabbitConsumer()
        {
            _rabbitConnection = new RabbitConnection();
        }

        public RabbitConsumer(IRabbitConnection connection)
        {
            _rabbitConnection = connection;
        }

        public void Subscribe(string routingKey, EventHandler<BasicDeliverEventArgs> receivedHandler)
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

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += receivedHandler;

                    channel.BasicConsume(queue: routingKey, autoAck: true, consumer: consumer);

                    Console.ReadLine();
                }
            }
        }

        public void Subscribe(string routingKey, Action<byte[]> receivedHandler)
        {
            using (var connection = _rabbitConnection.ConnectionFactory())
            {
                using (var channel = connection.CreateModel())
                {
                    using (var signal = new ManualResetEvent(false))
                    {
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (sender, args) =>
                        {
                            receivedHandler(args.Body);
                            signal.Set();
                        };

                        channel.BasicConsume(queue: routingKey, autoAck: false, consumer: consumer);
                        bool timeout = !signal.WaitOne(TimeSpan.FromSeconds(TaskTimeout));

                        channel.BasicCancel(consumer.ConsumerTag);
                        if (timeout)
                        {
                            throw new Exception("timeout");
                        }
                    }
                }
            }
        }
    }
}
