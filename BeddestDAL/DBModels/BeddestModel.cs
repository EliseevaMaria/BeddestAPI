namespace BeddestDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BeddestModel : DbContext
    {
        public BeddestModel()
            : base("name=BeddestDbModel")
        {
        }

        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Mode> Modes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
