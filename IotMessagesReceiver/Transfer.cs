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
        public static string connectionString = "HostName=Beddest.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=b7l4TnCxlCnmaGBGuJuCxT17r45JlWiV4qIENjlJu2c=";
        public static ServiceClient serviceClient;
        
        public static void Start()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
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
        }
    }
}
