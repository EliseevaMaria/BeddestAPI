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
        public static string GetDeviceName(int bedId)
        {
            string result = "";
            using (var context = new BeddestModel())
            {
                context.Devices.Load();
                result = (from device in context.Devices
                          where device.BedId == bedId
                          select device.DeviceName).FirstOrDefault() as string;
            }
            return result.TrimEnd(new char[] {' '});
        }

        public static void AddDevice(Device newDevice)
        {
            using (var context = new BeddestModel())
            {
                context.Devices.Load();
                context.Devices.Add(newDevice/*new Device { DeviceName = "Bed" + newDevice.BedId, BedId = newDevice.BedId }*/);
                context.SaveChanges();
            }
        }

        public static List<Device> GetDevices()
        {
            List<Device> result;
            using (var context = new BeddestModel())
            {
                context.Devices.Load();
                result = context.Devices.ToList();
            }
            return result;
        }

        public static void SetClientDeviceKey(int deviceId, string clientDeviceKey)
        {
            using (var context = new BeddestModel())
            {
                context.Devices.Load();
                var deviceToChange = context.Devices.Find(deviceId);
                if (deviceToChange != null)
                {
                    deviceToChange.ClientDeviceKey = clientDeviceKey;
                    context.SaveChanges();
                }
            }
        }
    }
}
