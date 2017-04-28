using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BlockDTO
    {
        public int Id;

        public int Height;
        public int TiltAngle;

        public int CurrentHardness;
        public int MinHardness;
        public int MaxHardness;

        public BlockDTO()
        {

        }

        public BlockDTO(int id, int heigth, int tiltAngle, int hardnessLevel, int currentHardness)
        {
            Id = id;
            Height = heigth;
            TiltAngle = tiltAngle;

            MinHardness = (hardnessLevel - 1) * 20 + 1;
            MaxHardness = hardnessLevel * 20;
            CurrentHardness = (hardnessLevel - 1) * 20 + currentHardness;
        }

        public BlockDTO(int blockId, int height, int tiltAngle, int hardness)
        {
            Id = blockId;
            Height = height;
            TiltAngle = tiltAngle;
            CurrentHardness = hardness;
            int level = (hardness - 1) / 20;
            MinHardness = level * 20 + 1;
            MaxHardness = MinHardness + 19;
        }
    }
}
