using BeddestDAL;
using Microsoft.Azure.Devices;
using Microsoft.ServiceBus.Messaging;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IotMessagesReceiver
{
    public class Program
    {
        /*static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        private static async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
            }
        }*/

        public static void Main(string[] args)
        {
            Console.WriteLine("Send Cloud-to-Device message\n");
            Transfer.serviceClient = ServiceClient.CreateFromConnectionString(Transfer.connectionString);

            Console.WriteLine("Press any key to send a C2D message.");
            Console.ReadLine();
            Transfer.SendC2DAsync("FirstIotBed", "SetTemp", 25).Wait();
            Console.ReadLine();

            //Transfer.ReceiveD2CAsync().Wait();

            /*if (Transfer.eventHubClient == null)
                Transfer.eventHubClient = EventHubClient.CreateFromConnectionString(Transfer.connectionString, Transfer.iotHubD2cEndpoint);
            var d2cPartitions = Transfer.eventHubClient.GetRuntimeInformation().PartitionIds;

            var eventHubReceivers = new List<EventHubReceiver>();
            foreach (string partition in d2cPartitions)
            {
                eventHubReceivers.Add(Transfer.eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow));
            }
            Console.WriteLine("we");
            Console.ReadLine();

            var tasks = new List<Task>();
            foreach (var eventHubReceiver in eventHubReceivers)
            {
                tasks.Add(Transfer.ReceiveMessage(eventHubReceiver));
            }
            Task.WaitAny(tasks.ToArray());

            tasks = new List<Task>();
            foreach (var eventHubReceiver in eventHubReceivers)
            {
                tasks.Add(Transfer.ReceiveMessage(eventHubReceiver));
            }
            Task.WaitAny(tasks.ToArray());



            /*Console.WriteLine("Receive messages. Ctrl-C to exit.\n");
            eventHubClient = EventHubClient.CreateFromConnectionString(Transfer.connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            CancellationTokenSource cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, cts.Token));
            }
            Task.WaitAll(tasks.ToArray());*/

        }
        //struct Command
        //{
        //    public string Name;
        //    public string Data;

        //    public Command(string name, string data)
        //    {
        //        Name = name;
        //        Data = data;
        //    }
        //}

        //static void Main(string[] args)
        //{
        //    //Transfer.Start();
        //    //Transfer.ReceiveFeedbackAsync();


        //    Console.WriteLine("smth");
        //    Console.ReadLine();
        //    /*Mode data = ModeDataAccess.GetMode(2);
        //    Command command = new Command("SelectMode", JsonConvert.SerializeObject(data));

        //    Console.WriteLine("Send Cloud-to-Device message\n");
        //    for (int i = 0; ; i++)
        //    {
        //        //Transfer.SendAsync("myFirstDevice", JsonConvert.SerializeObject(command)).Wait();
        //        Console.ReadLine();
        //    }*/
        //    //sdf();
        //}

    }
}
