using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class ControllerViewModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Attributes { get; set; }
    }    
    public class ControllerRoleViewModel
    {
        public List<AspRoleController> ControllerSelecteds { get; set; }
        public List<AspController> Controllers { get; set; }
        public AspNetRole Roles { get; set; }
    }
}