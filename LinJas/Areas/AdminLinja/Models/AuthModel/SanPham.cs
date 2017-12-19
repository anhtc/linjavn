namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using LinJas.Areas.AdminLinja.Common;

    [Table("anhtc.SanPham")]
    public partial class SanPham
    {
        public Guid Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(170)]
        public string TieuDe { get; set; }

        [StringLength(170)]
        public string MoTa { get; set; }

        [StringLength(170)]
        public string TuKhoa { get; set; }

        public int GiaCu { get; set; }

        public int GiaMoi { get; set; }
        public string Gia
        {
            get
            {
                string txtGia = "";

                return txtGia += StringHelper.ToCurrency(GiaMoi) + " vnđ";
            }
        }
        public int? ChietKhau { get; set; }

        public int? KhuyenMaiId { get; set; }

        public string NgayBatDau { get; set; }

        public string NgayKetThuc { get; set; }

        public int? DanhMucId { get; set; }

        public int? SapSep { get; set; }        
        public int TrangThai { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public bool Active { get; set; }

        public string NgayTao { get; set; }

        public byte[] HinhAnh { get; set; }
        public string Anh { get; set; }
        public string TuKhoaTimKiem { get; set; }
        public string Activeted
        {
            get
            {
                string trangthai = "";
                if (Active == true)
                {
                    trangthai += "<span style=\"color:#02bb10;\" class=\"glyphicon glyphicon-check\"></span>";
                }
                else
                {
                    trangthai += "<span  style=\"color:#d02028;\" class=\"glyphicon glyphicon-lock\"></span>";
                }
                return trangthai;
            }
        }
        public string UrlAnh
        {
            get
            {
                return "Common/ShowPhotoSanPhamById/" + Id;
            }
        }
    }
}
