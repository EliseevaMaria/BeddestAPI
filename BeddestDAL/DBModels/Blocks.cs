namespace BeddestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Models;

    public partial class Block
    {
        public int BlockId { get; set; }

        public int BedId { get; set; }

        public int Height { get; set; }

        public int TiltAngle { get; set; }

        public int Hardness { get; set; }

        public int HardnessLevel { get; set; }

        public virtual Bed Bed { get; set; }
        
        public Block(int bedId, int hardnessLevel)
        {
            BedId = bedId;
            Height = 0;
            TiltAngle = 0;
            HardnessLevel = hardnessLevel;
            Hardness = HardnessLevel * 20 - 10;
        }

        public Block()
        {

        }

        public Block(BlockDTO dto)
        {
            BlockId = dto.Id;
            BedId = 0;
            Height = dto.Height;
            TiltAngle = dto.TiltAngle;
            Hardness = dto.CurrentHardness - dto.MinHardness + 1;
            HardnessLevel = dto.MaxHardness / 20;
        }

        public BlockDTO ToDto()
        {
            return new BlockDTO(BlockId, Height, TiltAngle, HardnessLevel, Hardness);
        }
    }
}
