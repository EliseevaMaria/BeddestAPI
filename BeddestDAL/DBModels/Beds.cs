namespace BeddestDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Beds
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Beds()
        {
            Blocks = new HashSet<Blocks>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int Temperature { get; set; }

        public DateTime AlarmTime { get; set; }

        public int StopHeatingTime { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blocks> Blocks { get; set; }


        public Beds(int id, int userId, string name, int temp = 23, DateTime alarmTime = new DateTime(), int stopHeatingTime = 5)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Temperature = temp;
            AlarmTime = alarmTime == new DateTime() ? new DateTime(2001, 1, 1, 0, 0, 0) : alarmTime;
            StopHeatingTime = stopHeatingTime;
        }

        public BedDTO ToDto(int[] blockIds)
        {
            return new BedDTO(Id, Name, Temperature, AlarmTime, StopHeatingTime, blockIds);
        }
    }
}
