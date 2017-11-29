using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using System.Globalization;
using System.IO;
using System.Web.Optimization;

namespace LinJas.Common
{
    public static class HtmlHelperExtension
    {
        public static string CategoryItemActive(this HtmlHelper html, string actions = "", string controllers = "",
            string categorys = "", string cssClass = "active")
        {
            try
            {
                var num = 0;
                ViewContext viewContext = html.ViewContext;
                bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

                if (isChildAction)
                    viewContext = html.ViewContext.ParentActionViewContext;


                RouteValueDictionary routeValues = viewContext.RouteData.Values;
                string currentAction = routeValues["action"].ToString();
                string currentController = routeValues["controller"].ToString();

                string currentCategory = "";
                if (routeValues.ContainsKey("category"))
                {
                    currentCategory = routeValues["category"].ToString();
                }
                else if (int.TryParse(categorys, out num) && routeValues.ContainsKey("parentId"))
                {
                    currentCategory = routeValues["parentId"].ToString();
                }
                else if (routeValues.ContainsKey("tenLoaiBlog"))
                {
                    currentCategory = routeValues["tenLoaiBlog"].ToString();
                }
                else if (int.TryParse(categorys, out num) && routeValues.ContainsKey("loaiTinTucId"))
                {
                    currentCategory = routeValues["loaiTinTucId"].ToString();
                }
                else if (int.TryParse(categorys, out num) && routeValues.ContainsKey("tagId"))
                {
                    currentCategory = routeValues["tagId"].ToString();
                }
                else if (int.TryParse(categorys, out num) && routeValues.ContainsKey("nhomBSTId"))
                {
                    currentCategory = routeValues["nhomBSTId"].ToString();
                }
                if (String.IsNullOrEmpty(actions))
                    actions = currentAction;

                if (String.IsNullOrEmpty(controllers))
                    controllers = currentController;

                if (String.IsNullOrEmpty(categorys))
                    categorys = currentCategory;

                string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
                string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();
                string[] acceptedCategorys = categorys.Trim().Split(',').Distinct().ToArray();

                string str = acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) &&
                             acceptedCategorys.Contains(currentCategory)
                    ? cssClass
                    : String.Empty;

                return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) &&
                       acceptedCategorys.Contains(currentCategory)
                    ? cssClass
                    : String.Empty;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }


        public static string ReturnUrl(this HttpRequestBase request)
        {
            var returnUrl = request.RawUrl;
            if (!string.IsNullOrEmpty(request.QueryString["returnUrl"]))
            {
                returnUrl = request.QueryString["returnUrl"];
            }
            return returnUrl;
        }
      
        private static string LoadBundleContent(HttpContextBase httpContext, string bundleVirtualPath)
        {
            var bundleContext = new BundleContext(httpContext, BundleTable.Bundles, bundleVirtualPath);
            var bundle = BundleTable.Bundles.Single(b => b.Path == bundleVirtualPath);
            var bundleResponse = bundle.GenerateBundleResponse(bundleContext);

            return bundleResponse.Content;
        }

        private static IHtmlString InlineBundle(this HtmlHelper htmlHelper, string bundleVirtualPath, string htmlTagName)
        {
            string bundleContent = LoadBundleContent(htmlHelper.ViewContext.HttpContext, bundleVirtualPath);
            string htmlTag = string.Format("<{0}>{1}</{0}>", htmlTagName, bundleContent);

            return new HtmlString(htmlTag);
        }

        public static IHtmlString InlineScripts(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, "script");
        }

        public static IHtmlString InlineStyles(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, "style");
        }      
        /* Bread crum */
        public static string Titleize(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text).ToSentenceCase();
        }
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
    }
}
