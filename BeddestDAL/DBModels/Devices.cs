namespace BeddestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Devices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BedId { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceId { get; set; }
    }
}
