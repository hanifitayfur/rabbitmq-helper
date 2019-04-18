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

            // Using first sample
            consumer.Subscribe(Queens.QueenTopic, Consumer_Received_Sample_Body);

            // Using second sample
            //consumer.Subscribe(Queens.QueenTopic, Consumer_Received_Sample_EventHandler);

            Console.ReadKey();
        }

        public static void Consumer_Received_Sample_Body(byte[] body)
        {
            var message = Encoding.UTF8.GetString(body);
            var deseriliazeObject = JsonConvert.DeserializeObject<SampleDataModel>(message);
            Console.WriteLine(JsonConvert.SerializeObject(deseriliazeObject));
        }

        public static void Consumer_Received_Sample_EventHandler(object sender, BasicDeliverEventArgs args)
        {
            var message = Encoding.UTF8.GetString(args.Body);
            var deseriliazeObject = JsonConvert.DeserializeObject<SampleDataModel>(message);
            Console.WriteLine(JsonConvert.SerializeObject(deseriliazeObject));
        }
    }
}
