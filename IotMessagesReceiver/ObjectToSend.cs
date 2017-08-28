using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotMessagesReceiver
{
    public static class ObjectToSend
    {
        internal struct Command
        {
            public string Name;
            public object Data;

            public Command(string name, object data)
            {
                Name = name;
                Data = data;
            }
        }
    }
}
