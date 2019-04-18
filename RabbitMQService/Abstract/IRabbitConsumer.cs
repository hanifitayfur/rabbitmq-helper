using System;
using RabbitMQ.Client.Events;

namespace RabbitMQService.Abstract
{
    public interface IRabbitConsumer
    {
        void Subscribe(string routingKey, EventHandler<BasicDeliverEventArgs> receivedHandler);
        void Subscribe(string routingKey, Action<byte[]> receivedHandler);
    }
}
