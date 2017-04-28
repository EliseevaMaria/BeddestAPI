using Microsoft.ServiceBus.Messaging;
using BusinessLayer;
//using IotMessagesReceiver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeddestAPI.Controllers
{
    public class HomeController : Controller
    {
        public static EventHubClient eventHubClient;
        public static string iotHubD2cEndpoint = "messages/events";
        public string Index()
        {
            return "wer";
            //try
            //{
            //    Transfer.Start();
            //    Task.Factory.StartNew(() =>
            //    {
            //        eventHubClient = EventHubClient.CreateFromConnectionString(Transfer.connectionString, iotHubD2cEndpoint);

            //        var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            //        var tasks = new List<Task>();
            //        foreach (string partition in d2cPartitions)
            //        {
            //            tasks.Add(ReceiveMessagesFromDeviceAsync(partition));
            //        }
            //        Task.WaitAll(tasks.ToArray());
            //    });
            //}
            //catch { }
        }

        private static async Task ReceiveMessagesFromDeviceAsync(string partition)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
                Process(data);
            }
        }

        struct Command
        {
            public string Name;
            public string Data;

            public Command(string name, string data)
            {
                Name = name;
                Data = data;
            }
        }
        private static void Process(string received)
        {
            if (received != null)
            {
                try
                {
                    Command command = JsonConvert.DeserializeObject<Command>(received);
                    switch (command.Name)
                    {
                        case "ModeParams":
                            string[] data = JsonConvert.DeserializeObject<string>(command.Data).Split(' ');
                            CtrlToDal.SelectModeForDB(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]));
                            break;
                        case "AddMode":
                            string[] mattressData = JsonConvert.DeserializeObject<string>(command.Data).Split(' ');
                            CtrlToDal.AddModeForDB(int.Parse(mattressData[0]), int.Parse(mattressData[1]), mattressData[2], int.Parse(mattressData[3]), int.Parse(mattressData[4]));
                            break;
                    }
                }
                catch (JsonSerializationException)
                { }
            }
        }
    }
}
