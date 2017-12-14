using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinJas.Areas.AdminLinja.Common;

namespace LinJas.Areas.AdminLinja.Models
{
    public class SanPhamModel
    {
        public Guid Id {get;set;}
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public int GiaMoi { get; set; }
        public string Gia
        {
            get
            {
                string  txtGia="";
                
                return txtGia += StringHelper.ToCurrency(GiaMoi) +" vnđ"; 
            }
        }
		public int ChietKhau { get; set; }
		public int SapSep { get; set; }
		public string TrangThai { get; set; }
		public  bool Active { get; set; }
		public byte[] HinhAnh { get; set; }
        public string Anh { get; set; }
		public string Ten { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string NgayTao { get; set; }
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