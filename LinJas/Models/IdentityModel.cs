using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Models
{
    public class IdentityModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int Tuoi { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public string UrlPhoto { get; set; }
        public string Phone { get; set; }
    }
}