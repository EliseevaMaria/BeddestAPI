using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BedDTO
    {
        public int Id;
        public string Name;
        public int Temperature;
        public DateTime AlarmTime;
        public int StopHeatingTime;
        public int[] BlockIds;

        public BedDTO()
        {

        }

        /*public BedDTO(Bed bed, List<BlockDTO> blocks)
        {
            Id = bed.Id;
            Name = bed.Name;
            Temperature = bed.Temperature;
            AlarmTime = bed.AlarmTime;
            StopHeatingTime = bed.StopHeatingTime;

            int blocksCount = blocks.Count();
            BlockIds = new int[blocksCount];
            for (int i = 0; i < blocksCount; i++)
            {
                BlockIds[i] = blocks[i].Id;
            }
        }*/

        public BedDTO(int id, string name, int temp, DateTime alarm, int heatingTime, int[] blockIds)
        {
            Id = id;
            Name = name;
            Temperature = temp;
            AlarmTime = alarm;
            StopHeatingTime = heatingTime;
            BlockIds = blockIds;
        }
    }
}
