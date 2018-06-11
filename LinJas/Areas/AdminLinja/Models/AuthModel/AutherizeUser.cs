using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class AutherizeUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] listpermission = { "Administrator" };//Get list quyền cho user
            string role = "Administrator";// check quyền người dùng, nếu là admin thì không hỏi quyền, trường  hợp là  user thì check list quyền

            if (!listpermission.Contains(role))
            {
                filterContext.Result = new RedirectResult("~/Admin/Administrator/NotificationAutherize");
            }
        }
    }
}