namespace LinJas.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AsbNetModel : DbContext
    {
        public AsbNetModel()
            : base("name=AsbNetModel")
        {
        }

        public virtual DbSet<AsbController> AsbControllers { get; set; }
        public virtual DbSet<AsbNetRole> AsbNetRoles { get; set; }
        public virtual DbSet<AsbNetUser> AsbNetUsers { get; set; }
        public virtual DbSet<AsbRoleController> AsbRoleControllers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsbNetUser>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<AsbNetUser>()
                .Property(e => e.PhoneNumberConfirmed)
                .IsFixedLength();
        }
    }
}
