namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspController")]
    public partial class AspController
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(150)]
        public string Controller { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(50)]
        public string Area { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public bool? IsDelete { get; set; }
    }
}
