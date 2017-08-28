using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModeDTO
    {
        public int ModeId { get; set; }
        
        public string Name { get; set; }

        public int HeadHeight { get; set; }

        public int HeadTilt { get; set; }

        public int HeadHardness { get; set; }

        public int LegsHeight { get; set; }

        public int LegsTilt { get; set; }

        public int LegsHardness { get; set; }

        public int OtherHeight { get; set; }

        public int OtherHardness { get; set; }

        public ModeDTO()
        {

        }

        public ModeDTO(int id, string name)
        {
            ModeId = id;
            Name = name;
        }
        public ModeDTO(int modeId, string name, int headHeight, int headTilt, int headHardness, int legHeight, int legTilt, int legHardness, int otherHeight, int otherHardness)
        {
            ModeId = modeId;
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

        /*public ModeDTO(Mode mode)
        {
            Id = mode.Id;
            Name = mode.Name;
        }*/
    }
}
