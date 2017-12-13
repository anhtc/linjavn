using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models
{
    public class SanPhamModel
    {
        public Guid Id {get;set;}
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public float GiaMoi { get; set; }
		public int ChietKhau { get; set; }
		public int SapSep { get; set; }
		public string TrangThai { get; set; }
		public  bool Active { get; set; }
		public byte[] HinhAnh { get; set; }
		public string Ten { get; set; }
    }
}