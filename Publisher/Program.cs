using System;
using Models;
using RabbitMQService;
using RabbitMQService.Abstract;

namespace _PublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IRabbitPublisher _publisher = new RabbitPublisher();

            _publisher.PublishQueen(new SampleDataModel
            {
                Id = 1,
                Age = 30,
                Name = "Hanifi",
                Surname = "Tayfur",
                QueenGuid = Guid.NewGuid()
            });

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
