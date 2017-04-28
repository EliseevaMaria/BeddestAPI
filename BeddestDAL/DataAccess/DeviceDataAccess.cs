using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeddestDAL
{
    public class DeviceDataAccess
    {
        public static string GetDeviceId(int bedId)
        {
            string result = "";
            using (var context = new BeddestModel())
            {
                context.Devices.Load();
                result = (from device in context.Devices
                          where device.BedId == bedId
                          select device.DeviceId).FirstOrDefault() as string;
            }
            return result;
        }
    }
}
