using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Models
{
    public class ControllerModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Attributes { get; set; }
    }
    public class ControllerRoleModel
    {
        public List<AsbRoleController> ControllerSelecteds { get; set; }
        public List<AsbController> Controller { get; set; }
        public AsbNetRole Roles { get; set; }
    }
}