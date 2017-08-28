namespace BeddestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Device
    {
        [Key]
        public int DeviceId { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceName { get; set; }
        
        public int BedId { get; set; }

        [StringLength(50)]
        public string ClientDeviceKey { get; set; }

        public virtual Bed Bed { get; set; }

        public Device()
        {

        }

        public Device(int bedId)
        {
            BedId = bedId;
            DeviceName = "Bed" + bedId;
        }
    }
}
