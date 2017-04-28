namespace BeddestDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BeddestModel : DbContext
    {
        public BeddestModel()
            : base("name=BeddestModels")
        {
        }

        public virtual DbSet<Beds> Beds { get; set; }
        public virtual DbSet<Blocks> Blocks { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Modes> Modes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beds>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Beds>()
                .HasMany(e => e.Blocks)
                .WithRequired(e => e.Beds)
                .HasForeignKey(e => e.BedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .Property(e => e.DeviceId)
                .IsFixedLength();

            modelBuilder.Entity<Modes>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Beds)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Modes)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
