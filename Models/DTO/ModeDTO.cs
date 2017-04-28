using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModeDTO
    {
        public int Id;
        public string Name;

        public ModeDTO()
        {

        }

        public ModeDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /*public ModeDTO(Mode mode)
        {
            Id = mode.Id;
            Name = mode.Name;
        }*/
    }
}
