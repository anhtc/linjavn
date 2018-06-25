using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class DichVu
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public bool Active { get; set; }
        public int OrderBy { get; set; }
        public string MetaTieuDe { get; set; }
        public string MetaMoTa { get; set; }
        public string MetaTuKhoa { get; set; }
        public string HinhAnh
        {
            get
            {
                return "Common/ShowPhotoDichVuById/" + Id;
            }
        }
        public int DanhGia { get; set; }
        public int SoNguoi { get; set; }
        public string GhiChu { get; set; }
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