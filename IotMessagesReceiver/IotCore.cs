using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace IotMessagesReceiver
{
    public class IotCore
    {
        static RegistryManager registryManager;
        static string connectionString = "HostName=beddestIotHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=O6jzdunfohqo49ptZ25T5BHKOXXVGJlwYsUgrdhNLoo=";

        public static void CreateRegistryManager()
        {
            if (registryManager == null)
                registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        }        
            
        public static async Task<string> AddDeviceAsync(string newDeviceName)
        {
            CreateRegistryManager();
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(newDeviceName));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(newDeviceName);
            }
            return device.Authentication.SymmetricKey.PrimaryKey;
        }

        public static async Task<bool> RemoveDeviceAsync(string deviceName)
        {
            CreateRegistryManager();
            Device device;
            try
            {
                device = await registryManager.GetDeviceAsync(deviceName);
                await registryManager.RemoveDeviceAsync(device);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
