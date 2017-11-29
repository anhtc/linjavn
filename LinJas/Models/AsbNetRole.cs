namespace LinJas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AsbNetRole
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }
    }
}
