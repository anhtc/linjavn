using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Areas.AdminLinja.Models
{
    public class AutherizeUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string role = "";
            if (HttpContext.Current.Session.Count > 0)
                {
                   role += HttpContext.Current.Session["role"].ToString();
                }

            else
            {
                role += "User";
            }
           if (!role.Contains("administrator"))
            {
                filterContext.Result = new RedirectResult("~/Admin/Administrator/NotificationAutherize");
            }
        }
    }
}