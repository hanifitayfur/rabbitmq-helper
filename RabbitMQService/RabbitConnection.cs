using RabbitMQ.Client;
using RabbitMQService.Abstract;

namespace RabbitMQService
{
    public class RabbitConnection : IRabbitConnection
    {
        // test    
        private readonly string _hostName = "localhost";
        private readonly int _port = 8081;


        public IConnection ConnectionFactory()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = _port
            };

            return connectionFactory.CreateConnection();
        }

        public IConnection ConnectionFactory(string hostname)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = hostname
            };

            return connectionFactory.CreateConnection();
        }

        public IConnection ConnectionFactory(string hostname, int port)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = hostname,
                Port = port
            };

            return connectionFactory.CreateConnection();
        }
    }
}
