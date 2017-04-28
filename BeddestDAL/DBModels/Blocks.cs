namespace BeddestDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blocks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int BedId { get; set; }

        public int Height { get; set; }

        public int TiltAngle { get; set; }

        public int Hardness { get; set; }

        public int HardnessLevel { get; set; }

        public virtual Beds Beds { get; set; }


        public Blocks(int id, int bedId, int hardnessLevel)
        {
            Id = id;
            BedId = bedId;
            Height = 0;
            TiltAngle = 0;
            HardnessLevel = hardnessLevel;
            Hardness = HardnessLevel * 20 - 10;
        }

        public Blocks()
        {

        }

        public Blocks(BlockDTO dto)
        {
            Id = dto.Id;
            BedId = 0;
            Height = dto.Height;
            TiltAngle = dto.TiltAngle;
            Hardness = dto.CurrentHardness - dto.MinHardness + 1;
            HardnessLevel = dto.MaxHardness / 20;
        }

        public BlockDTO ToDto()
        {
            return new BlockDTO(Id, Height, TiltAngle, HardnessLevel, Hardness);
        }
    }
}
