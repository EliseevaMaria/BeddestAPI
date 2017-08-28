using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DeviceDTO
    {
        public int DeviceId { get; set; }
        
        public string DeviceName { get; set; }

        public int BedId { get; set; }
        
        public string ClientDeviceKey { get; set; }

        public DeviceDTO()
        {

        }

        public DeviceDTO(int id, string name, int bedId, string cleintDeviceKey)
        {
            DeviceId = id;
            DeviceName = name;
            BedId = bedId;
            ClientDeviceKey = cleintDeviceKey;
        }
    }
}
