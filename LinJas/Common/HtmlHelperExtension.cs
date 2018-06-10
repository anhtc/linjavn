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
        private static readonly ILog Logger = LogManager.GetLogger(typeof(HtmlHelperExtension));
        public static string MenuItemActive(this HtmlHelper html, string actions, string controllers, int? id = null, string cssClass = "active")
        {
            try
            {
                var viewContext = html.ViewContext;
                var isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

                if (isChildAction)
                    viewContext = html.ViewContext.ParentActionViewContext;

                var routeValues = viewContext.RouteData.Values;
                var currentAction = routeValues["action"].ToString();
                var currentController = routeValues["controller"].ToString();

                int? currentId = null;
                if (routeValues["id"] != null)
                {
                    currentId = Convert.ToInt32(routeValues["id"]);
                }

                if (string.IsNullOrEmpty(actions))
                    actions = currentAction;

                if (string.IsNullOrEmpty(controllers))
                    controllers = currentController;

                if (!id.HasValue)
                {
                    currentId = id;
                }

                var acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
                var acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

                return acceptedActions.Contains(currentAction)
                    && acceptedControllers.Contains(currentController)
                    && currentId == id
                    ? cssClass
                    : string.Empty;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static bool IsActionName(this HtmlHelper html, string actionName, string controllerName)
        {
            var controllerContext = html.ViewContext.Controller.ControllerContext;
            if (controllerContext.IsChildAction) return false;

            var currentActionName = controllerContext.RouteData.Values["action"].ToString();
            var currentControllerName = controllerContext.RouteData.Values["controller"].ToString();

            return string.Equals(currentActionName, actionName, StringComparison.OrdinalIgnoreCase)
                && string.Equals(currentControllerName, controllerName, StringComparison.OrdinalIgnoreCase);
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
