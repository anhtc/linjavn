namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ManagerModel : DbContext
    {
        public ManagerModel()
            : base("name=ManagerModel")
        {
        }

        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Tinh> Tinhs { get; set; }
        public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
