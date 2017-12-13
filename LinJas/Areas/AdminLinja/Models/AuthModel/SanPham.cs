namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("anhtc.SanPham")]
    public partial class SanPham
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(170)]
        public string TieuDe { get; set; }

        [StringLength(170)]
        public string MoTa { get; set; }

        [StringLength(170)]
        public string TuKhoa { get; set; }

        public double? GiaCu { get; set; }

        public double? GiaMoi { get; set; }

        public int? ChietKhau { get; set; }

        public int? KhuyenMaiId { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? DanhMucId { get; set; }

        public int? SapSep { get; set; }

        [StringLength(150)]
        public string TrangThai { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public bool? Active { get; set; }

        public DateTime? NgayTao { get; set; }

        public byte[] HinhAnh { get; set; }
    }
}
