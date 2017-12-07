namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetUser")]
    public partial class AspNetUser
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

        [StringLength(250)]
        public string Avatar { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Active { get; set; }

        public Guid? RoleId { get; set; }

        public string Hoten { get; set; }
    }
}
