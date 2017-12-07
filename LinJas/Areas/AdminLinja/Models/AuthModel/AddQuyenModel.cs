using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class AddQuyenModel
    {      
        public Guid RoleId { get; set; }       
        public string Controller { get; set; }       
        public string Action { get; set; }        
        public string Description { get; set; }
        public string RemoveQuyen { get; set; }
    }
}