using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class Blog
    {
        public Guid? id { get; set; }
		public int? DanhMucId { get; set; }
		public string Name { get; set; }
		public string NoiDung { get; set; }
		public string TieuDe { get; set; }
		public string MoTa { get; set; }
		public string TuKhoa { get; set; }
		public int? SapXep { get; set; }
		public DateTime? NgayTao { get; set; }
		public DateTime? NgayDang { get; set; }
		public byte[] HinhAnh { get; set; }
		public int ArticleId { get; set; }
		public bool Active { get; set; }
        public string Anh { get; set; }
        public string UrlAnh
        {
            get
            {
                return "Common/ShowPhotoBlogById/" + id;
            }
        }
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
    }
}