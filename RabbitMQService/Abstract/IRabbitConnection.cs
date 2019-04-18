using RabbitMQ.Client;

namespace RabbitMQService.Abstract
{
    public interface IRabbitConnection
    {
        IConnection ConnectionFactory();

        IConnection ConnectionFactory(string hostname);

        IConnection ConnectionFactory(string hostname, int port);
    }
}
