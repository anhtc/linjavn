namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AsbController")]
    public partial class AsbController
    {
        [Required]
        [StringLength(250)]
        public string Controller { get; set; }

        [Key]
        [StringLength(150)]
        public string Action { get; set; }

        [StringLength(150)]
        public string Area { get; set; }

        [StringLength(350)]
        public string Description { get; set; }

        public bool? IsDelete { get; set; }
    }
}
