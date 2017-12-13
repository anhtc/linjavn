namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("anhtc.DanhMuc")]
    public partial class DanhMuc
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string Ma { get; set; }

        [StringLength(150)]
        public string Ten { get; set; }

        public int? SapSep { get; set; }

        public int? TinhId { get; set; }
    }
}
