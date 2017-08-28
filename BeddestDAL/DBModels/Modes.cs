namespace BeddestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Models;

    public partial class Mode
    {
        public int ModeId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int HeadHeight { get; set; }

        public int HeadTilt { get; set; }

        public int HeadHardness { get; set; }

        public int LegsHeight { get; set; }

        public int LegsTilt { get; set; }

        public int LegsHardness { get; set; }

        public int OtherHeight { get; set; }

        public int OtherHardness { get; set; }

        public virtual User User { get; set; }

        public Mode()
        {

        }

        public Mode(int userId, string name, int headHeight, int headTilt, int headHardness, int legHeight, int legTilt, int legHardness, int otherHeight, int otherHardness)
        {
            UserId = userId;
            Name = name;
            HeadHeight = headHeight;
            HeadTilt = headTilt;
            HeadHardness = headHardness;
            LegsHeight = legHeight;
            LegsTilt = legTilt;
            LegsHardness = legHardness;
            OtherHeight = otherHeight;
            OtherHardness = otherHardness;
        }

        public ModeDTO ToDto()
        {
            return new ModeDTO(ModeId, Name);
        }
    }
}
