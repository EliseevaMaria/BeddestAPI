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
    class Program
    {
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

        static void Main(string[] args)
        {
            //Transfer.Start();
            //Transfer.ReceiveFeedbackAsync();
            

            Console.WriteLine("smth");
            Console.ReadLine();
            /*Mode data = ModeDataAccess.GetMode(2);
            Command command = new Command("SelectMode", JsonConvert.SerializeObject(data));

            Console.WriteLine("Send Cloud-to-Device message\n");
            for (int i = 0; ; i++)
            {
                //Transfer.SendAsync("myFirstDevice", JsonConvert.SerializeObject(command)).Wait();
                Console.ReadLine();
            }*/
            //sdf();
        }
        
    }
}
