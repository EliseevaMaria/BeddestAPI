using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserDTO
    {
        private int id;
        private string name;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public UserDTO()
        {

        }

        /*public UserDTO(User user)
        {
            if (user != null)
            {
                Id = user.Id;
                Name = user.Name;
            }
        }*/

        public UserDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
