using BeddestDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class Services
    {
        public static Modes CreateMode(int userId, int head, int legs, string name, Blocks[] blocks)
        {
            int avgHeight = 0, avgHardness = 0;
            for (int i = 0; i < blocks.Length; i++)
            {
                if (i == head || i == legs)
                {
                    continue;
                }
                avgHardness += (blocks[i].Hardness);
                avgHeight += blocks[i].Height;
            }
            avgHardness /= (blocks.Length / 2);
            avgHeight /= (blocks.Length / 2);

            Modes newMode = new Modes(userId, name,
                blocks[head].Height, blocks[head].TiltAngle, blocks[head].Hardness,
                blocks[legs].Height, blocks[legs].TiltAngle, blocks[legs].Hardness,
                avgHeight != 0 ? avgHeight : 1, avgHardness != 0 ? avgHardness : 1);

            return newMode;
        }
    }
}
