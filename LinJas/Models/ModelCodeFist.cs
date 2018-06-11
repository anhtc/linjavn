namespace LinJas.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCodeFist : DbContext
    {
        public ModelCodeFist()
            : base("name=ModelCodeFist")
        {
        }

        public virtual DbSet<AspController> AspControllers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspRoleController> AspRoleControllers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Phone)
                .IsFixedLength();
        }
    }
}
