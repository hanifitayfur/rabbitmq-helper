using System;
using System.Text;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQService;
using RabbitMQService.Abstract;

namespace _ConsumerApp
{
    class Program
    {

        public delegate void delmethod(object sender, BasicDeliverEventArgs e);

        static void Main(string[] args)
        {
            IRabbitConsumer consumer = new RabbitConsumer();
            consumer.Subscribe(Queens.QueenTopic, Consumer_Received_Sample);

            Console.ReadKey();
        }

        public static void Consumer_Received_Sample(byte[] body)
        {
            var message = Encoding.UTF8.GetString(body);
            var deseriliazeObject = JsonConvert.DeserializeObject<SampleDataModel>(message);
            Console.WriteLine(JsonConvert.SerializeObject(deseriliazeObject));
        }
    }
}
