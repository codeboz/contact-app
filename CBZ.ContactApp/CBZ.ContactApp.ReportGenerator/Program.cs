using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CBZ.ContactApp.Data.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

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
                GenerateReport(config["Url:contactapp"],msg);
            };
            channel.BasicConsume("reportGenerationQueue", true, consumer);
            Console.ReadLine();
            channel.Close();
            connection.Close();
        }

        private static void GenerateReport(string contactApp,string msg)
        {
            ReportRequest reportRequest = JsonConvert.DeserializeObject<ReportRequest>(msg);
            var url = contactApp+"/v1/Infos?$filter=Data eq '" + reportRequest.Location + "'&$count=true";
            var getContactInfoByLocation = Get(url);
            JObject jolocationInfo = JObject.Parse(getContactInfoByLocation);
            int contactCount = jolocationInfo.GetValue("@odata.count")!.Value<int>();
            string insertedReport;
            if (contactCount == 0)
            {
                Report r = new Report {Location = reportRequest.Location, PhoneNumberCount = 0, ContactCount = 0};
                insertedReport=Post(contactApp,r);
            }
            else
            {
                Contact[] contacts = jolocationInfo.GetValue("value")!.Value<Contact[]>();
                var url2 = contactApp+"/v1/Infos?$filter=(InfoTypeId eq 1) and (";
                StringBuilder stringBuilder = new StringBuilder(url2);
                foreach (var contact in contacts)
                {
                    stringBuilder.Append("ContactId eq ")
                        .Append(contact.Id)
                        .Append(" or ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2) //Remove last or
                    .Append(")&$count=true");
                
                var getContactsPhoneNumbers = Get(stringBuilder.ToString());
                Console.WriteLine(getContactsPhoneNumbers);
                JObject joPhoneInfo = JObject.Parse(getContactsPhoneNumbers);
                int phoneCount = joPhoneInfo.GetValue("@odata.count")!.Value<int>();
                Report r = new Report {Location = reportRequest.Location, PhoneNumberCount = phoneCount, ContactCount = contactCount};
                insertedReport=Post(contactApp,r);
            }
            JObject joInsertedReport = JObject.Parse(insertedReport);
            Report report = joInsertedReport.GetValue("value")!.Value<Report>();
            reportRequest.ReportId = report.Id;
            reportRequest.ReportStateId = 2;
            Put(contactApp,reportRequest);
        }
        
        private static string Put(string baseUrl,ReportRequest reportRequest)
        {
            var client = new RestClient(baseUrl+"/v1/ReportRequests("+reportRequest.Id+")");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Prefer", "return=representation");
            request.AddParameter("application/json", JsonConvert.SerializeObject(reportRequest) ,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        private static string Post(string baseUrl,Report report)
        {
            var client = new RestClient(baseUrl+"/v1/Reports");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Prefer", "return=representation");
            request.AddParameter("application/json", JsonConvert.SerializeObject(report),  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        private static string Get(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }
    }
}
