using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace LinJas.Areas.AdminLinja.Models
{
    public class QuanHuyenModel
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TinhId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? SapXep { get; set; }
        public string TenTinh { get; set; }
    }
}