namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AsbRoleController")]
    public partial class AsbRoleController
    {
     
        public Guid RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string Controller { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(350)]
        public string Description { get; set; }
    }
}
