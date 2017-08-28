namespace BeddestDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Models;

    public partial class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        
        public virtual List<Bed> Beds { get; set; }
        
        public virtual List<Mode> Modes { get; set; }

        public User()
        {

        }
        
        public UserDTO ToDto()
        {
            return new UserDTO(UserId, Name);
        }

        public User(string userName, string password)
        {
            Name = userName;
            Password = password;
        }

        public User(int id, string userName, string password)
        {
            UserId = id;
            Name = userName;
            Password = password;
        }
    }
}
