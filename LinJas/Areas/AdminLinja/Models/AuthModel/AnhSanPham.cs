using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class AnhSanPham
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? SanPhamId { get; set; }
        public byte[] HinhAnh { get;set; }
        public DateTime? NgayTao { get; set; }
        public int SapXep { get; set; }
        public string Anh { get; set; }
        public string UrlAnh
        {
            get
            {
                return "Common/ShowPhotoAnhSanPham/" + Id;
            }
        }
    }
}