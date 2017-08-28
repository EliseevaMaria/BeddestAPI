namespace BeddestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Models;

    public partial class Bed
    {
        [Key]
        public int BedId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int Temperature { get; set; }

        public DateTime AlarmTime { get; set; }

        public int StopHeatingTime { get; set; }

        public virtual User User { get; set; }

        public virtual List<Block> Blocks { get; set; }

        public virtual List<Device> Device { get; set; }


        public Bed()
        {

        }

        public Bed(int userId, int temp = 23, DateTime alarmTime = new DateTime(), int stopHeatingTime = 5)
        {
            UserId = userId;
            Name = "Bed";
            Temperature = temp;
            AlarmTime = alarmTime == new DateTime() ? new DateTime(2001, 1, 1, 0, 0, 0) : alarmTime;
            StopHeatingTime = stopHeatingTime;
        }

        public Bed(int id, int userId, string name, int temp = 23, DateTime alarmTime = new DateTime(), int stopHeatingTime = 5)
        {
            BedId = id;
            UserId = userId;
            Name = name;
            Temperature = temp;
            AlarmTime = alarmTime == new DateTime() ? new DateTime(2001, 1, 1, 0, 0, 0) : alarmTime;
            StopHeatingTime = stopHeatingTime;
        }

        public BedDTO ToDto(int[] blockIds)
        {
            return new BedDTO(BedId, Name, Temperature, AlarmTime, StopHeatingTime, blockIds);
        }
    }
}
