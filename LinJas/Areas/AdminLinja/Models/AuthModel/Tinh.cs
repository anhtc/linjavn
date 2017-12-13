namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("anhtc.Tinh")]
    public partial class Tinh
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Sapsep { get; set; }
    }
}
