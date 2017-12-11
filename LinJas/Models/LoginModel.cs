using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinJas.Models
{
    public class LoginModel
    {
        public Guid Id { get; set; }

        [StringLength(350)]
        [Required(ErrorMessage ="Email Không được để  trống!")]
        public string Email { get; set; }

        [StringLength(550)]
        [Required( ErrorMessage = "Mật khẩu Không được để  trống!")]
        public string PasswordHash { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }

        [StringLength(150)]
        [Required( ErrorMessage = "Tài khoản Không được để  trống!")]
        public string UserName { get; set; }

        public byte[] Avatar { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Active { get; set; }

        public Guid? RoleId { get; set; }

        public string Hoten { get; set; }
        public string UrlAnh
        {
            get
            {
                return "AdminLinja/Roles/ShowPhotoById/" + Id;
            }
        }
    }
}