namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspRoleController")]
    public partial class AspRoleController
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string Controller { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(150)]
        public string Description { get; set; }
    }
}
