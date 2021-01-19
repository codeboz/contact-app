using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CBZ.ContactApp.Data.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Simple.OData.Client;

namespace CBZ.ContactApp.ReportGenerator
{
    class ReportGenerator
    {
        static void Main()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            IConfigurationRoot config = builder.Build();
            
            Console.WriteLine("Hello World! This is CBZ.ContactApp.ReportGenerator");
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(config["Url:rabbitmq"]);
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("contactAppExchange", ExchangeType.Fanout, true);
            channel.QueueDeclare("reportGenerationQueue", true, false, false);
            channel.QueueBind("reportGenerationQueue","contactAppExchange",""); //Fanout no need for routing key
            // declare resources here, handle consumed events, etc
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (_, eventArgs) =>
            {
                if (eventArgs == null) return;
                var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Console.WriteLine($"{msg}");
                //
#pragma warning disable 4014
                GenerateReport(config["Url:contactapp"],msg);
#pragma warning restore 4014
            };
            channel.BasicConsume("reportGenerationQueue", true, consumer);
            Console.ReadLine();
            channel.Close();
            connection.Close();
        }

        private static async Task GenerateReport(string contactApp, string msg)
        {
            try
            {
                ReportRequest reportRequest = JsonConvert.DeserializeObject<ReportRequest>(msg);

                var client = new ODataClient(contactApp + "/v1/");

                var infos = await client
                    .For<Info>()
                    .Filter(x => x.Data == reportRequest.Location)
                    .FindEntriesAsync();

                var enumerableContact = infos as Info[] ?? infos.ToArray();
                int contactCount = enumerableContact.Count();
                Report report;
                if (contactCount == 0)
                {
                    report  = await client
                        .For<Report>()
                        .Set(new Report
                        {
                            Location = reportRequest.Location,
                            ContactCount = 0,
                            PhoneNumberCount = 0
                        })
                        .InsertEntryAsync();
                }
                else
                {
                    var manuelfilter = contactApp + "/v1/Infos?$filter=(InfoTypeId eq 1) and (";
                    StringBuilder stringBuilder = new StringBuilder(manuelfilter);
                    foreach (var info in enumerableContact)
                    {
                        stringBuilder.Append("ContactId eq ")
                            .Append(info.ContactId.ToString())
                            .Append(" or ");
                    }

                    stringBuilder.Remove(stringBuilder.Length - 4, 4) //Remove last or
                        .Append(")");

                    var phones = await client
                        .FindEntriesAsync(stringBuilder.ToString());

                    int phoneCount = phones.Count();

                    report = await client
                        .For<Report>()
                        .Set(new Report
                        {
                            Location = reportRequest.Location,
                            ContactCount = contactCount,
                            PhoneNumberCount = phoneCount
                        })
                        .InsertEntryAsync();
                }
                
                    
                var unused = await client
                    .For<ReportRequest>()
                    .Key(reportRequest.Id)
                    .Set(new{ReportId=report.Id,ReportStateId=2})
                    .UpdateEntryAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
