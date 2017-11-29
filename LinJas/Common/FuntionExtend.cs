using LinJas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc.Filters;

namespace LinJas.Common
{
    public static class FuntionExtend
    {
        /// <summary>
        /// Kiểm tra phân quyền
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public static bool IsAuthenticated(this IIdentity identity, AuthenticationChallengeContext filterContext)
        {
            bool rs = false;
            using (AsbNetModel db=new AsbNetModel())
            {
                Guid userId = identity.GetUserId();
                AsbNetUser user = db.AsbNetUsers.Find(userId);
                if (user!=null)
                {
                    foreach (var item in db.AsbNetRoles)
                    {
                        rs = item.Isselected(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
                        if (rs)
                        {
                            return rs;
                        }
                    }
                }
                else
                return rs;
            }
            return rs;
        }
        /// <summary>
        /// Kiểm tra  xem quyền  đang dùng có được requet hay không?
        /// </summary>
        /// <param name="asbController"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static bool Isselected(this AsbNetRole asbController, string controller, string action)
        {
            using (AsbNetModel db = new AsbNetModel())
            {
                var roleController = db.AsbRoleControllers.Find(asbController.Id,controller,action);
                return roleController == null ? false : true;
            }
        }
    }
}