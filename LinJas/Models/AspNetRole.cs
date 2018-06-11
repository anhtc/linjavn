namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetRole")]
    public partial class AspNetRole
    {
        public Guid Id { get; set; }

        [StringLength(150)]
        public string RoleName { get; set; }
    }
}
