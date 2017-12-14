using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class NguoiDungModel
    {
        public Guid Id { get; set; }

        [StringLength(350)]
        public string Email { get; set; }

        [StringLength(550)]
        public string PasswordHash { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string UserName { get; set; }
      
        public byte[] Avatar { get; set; }
        public string Anh { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Active { get; set; }
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
                return "Roles/ShowPhotoById/" + Id;
            }
        }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Hoten { get; set; }
    }
}