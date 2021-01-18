using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Newtonsoft.Json;

namespace CBZ.ContactApp.ReportGenerator
{
    class ReportGenerator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! This is CBZ.ContactApp.ReportGenerator");
            var factory = new ConnectionFactory {Uri = new Uri("amqp://admin:secret@localhost:5672")};
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("reportGenerationQueue", true, false, false);
            var headers = new Dictionary<string, object>
            {
                {"subject", "report"},
                {"action", "generate"},
                {"x-match", "all"},
            };
            channel.QueueBind("reportGenerationQueue","contactAppExchange","",headers); //Fanout no need for routing key
            // declare resources here, handle consumed events, etc
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (_, eventArgs) =>
            {
                if (eventArgs == null) return;
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var subject = Encoding.UTF8.GetString((eventArgs.BasicProperties.Headers["subject"] as byte[])!);
                var action = Encoding.UTF8.GetString((eventArgs.BasicProperties.Headers["action"] as byte[])!);
                Console.WriteLine($"{subject} {action}: {msg}");
                ReportRequest deserializedProduct = JsonConvert.DeserializeObject<ReportRequest>(msg);
                //Todo: report Generation
                
                //Todo: Request Odata Endpoint To resolve report requirements
                

                int contactCount = 1;
                int phoneNumberCount = 1;

                    //Todo: Post generated Data into Odata endpoint
                System.Threading.Thread.Sleep(10000);
            };
            channel.BasicConsume("reportGenerationQueue", true, consumer);
            Console.ReadLine();
            channel.Close();
            connection.Close();
        }
    }

    internal class ODataClient
    {
        public ODataClient(string httpsLocalhostOdata)
        {
            throw new NotImplementedException();
        }
    }
}
