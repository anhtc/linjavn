using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace LinJas.Models
{
    public class AuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private bool _authenticate;


        public void OnAuthorization(AuthorizationContext filterContext)
        {
            _authenticate = (filterContext.ActionDescriptor.GetCustomAttributes(typeof(OverrideAuthenticationAttribute), true).Length == 0);
            throw new NotImplementedException();
        }

        public void AuthenticationChallenge(AuthenticationChallengeContext filterContext){
            var user = filterContext.HttpContext.User;
            if (user==null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}