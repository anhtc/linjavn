using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Models
{
    public class IdentityModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Active { get; set; }
        public Guid RoleId { get; set; }        
        public string Hoten { get; set; }       
    }
}