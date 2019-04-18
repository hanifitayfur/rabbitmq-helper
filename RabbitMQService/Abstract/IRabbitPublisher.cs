using Models;

namespace RabbitMQService.Abstract
{
    public interface IRabbitPublisher
    {
        void PublishQueen<T>(T data) where T : QueenModel, new();
        void PublishQueen<T>(string routingKey, T data) where T : QueenModel, new();
    }
}
