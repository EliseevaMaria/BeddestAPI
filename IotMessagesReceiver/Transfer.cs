using Microsoft.Azure.Devices;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotMessagesReceiver
{
    public static class Transfer
    {
        public static ServiceClient serviceClient;
        public static string connectionString = "HostName=beddest.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=ZhOCDIkTXu6Zkkh3yeoIRbabmDy3H/Ii59afRCUQXS4=";

        public async static Task SendC2DAsync(string deviceId, string commandName, object objectToSend)
        {
            if (serviceClient == null)
                serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            ObjectToSend.Command commandToSend = new ObjectToSend.Command(commandName, objectToSend);

            string messageString = JsonConvert.SerializeObject(commandToSend);
            Message commandMessage = new Message(Encoding.ASCII.GetBytes(messageString));
            await serviceClient.SendAsync(deviceId, commandMessage);
        }


        public static string iotHubD2cEndpoint = "messages/events";
        public static EventHubClient eventHubClient;
        public static List<EventHubReceiver> eventHubReceivers;

        public static void StartReceiving()
        {
            if (eventHubClient == null)
                CreateHubClient();
            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            eventHubReceivers = new List<EventHubReceiver>();
            foreach (string partition in d2cPartitions)
            {
                eventHubReceivers.Add(eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow));
            }
        }

        public static async Task ReceiveD2CAsync()
        {
            
        }

        public static async Task ReceiveMessage(EventHubReceiver eventHubReceiver)
        {
            Console.WriteLine(eventHubReceiver.PartitionId);
            while (true)
            {
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                fhfcg(eventHubReceiver.PartitionId, data);
                break;
            }
        }

        private static void fhfcg(string partition, string data)
        {
            //Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
        }

        public static void CreateHubClient()
        {
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
        }


        /*
        public static void Start()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }
        public static async Task SendAsync(bool send, string commandName, object objToSend)
        {
            try
            {
                if (!send)
                    return;

                Command command = new Command(commandName, JsonConvert.SerializeObject(objToSend));
                string messageString = JsonConvert.SerializeObject(command);

                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                Task.Factory.StartNew(() =>
                {
                    serviceClient.SendAsync("myFirstDevice", message);
                });
            }
            catch { }
        }*/
    }
}
