namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AsbNetUser")]
    public partial class AsbNetUser
    {
        public Guid Id { get; set; }

        [StringLength(350)]
        public string Email { get; set; }

        [StringLength(350)]
        public string EmailConfirmed { get; set; }

        [StringLength(350)]
        public string PasswordHash { get; set; }

        [StringLength(150)]
        public string SecurityStamp { get; set; }

        [StringLength(13)]
        public string Phone { get; set; }

        [StringLength(13)]
        public string PhoneNumberConfirmed { get; set; }

        [StringLength(150)]
        public string TowFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public DateTime? LockoutEnabled { get; set; }

        public int? AcessFalsedCount { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }
    }
}
