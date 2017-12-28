using System.Web;
using System.Web.Optimization;

namespace LinJas
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Desktop
            //common
            bundles.Add(new ScriptBundle("~/bundles/js/jquery.common.js").Include(
                    "~/js/jquery-1.10.2.min.js"));
            //Home
            bundles.Add(new ScriptBundle("~/bundles/js/home.index.desktop.js").Include(
                     "~/js/bootstrap.min.js",
                     "~/js/xingfa.common.desktop.js"));

            bundles.Add(new StyleBundle("~/css/home.index.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                     "~/css/xingfa.home.desktop.css",
                     "~/css/xingfa.common.desktop.css",
                     "~/css/xingfa.home.index.desktop.css"));

            //Detail product
            bundles.Add(new ScriptBundle("~/bundles/js/detail.desktop.js").Include(
                    "~/js/bootstrap.min.js",
                    "~/js/xingfa.common.desktop.js"));

            bundles.Add(new StyleBundle("~/css/detail.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                      "~/css/xingfa.common.desktop.css",
                     "~/css/xingfa.product.detail.desktop.css"));
            //Contact
            bundles.Add(new ScriptBundle("~/bundles/js/contact.desktop.js").Include(
                    "~/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/css/contact.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                     "~/css/linja.contact.desktop.css"));
            //About
            bundles.Add(new ScriptBundle("~/bundles/js/about.desktop.js").Include(
                    "~/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/css/about.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                     "~/css/linja.about.desktop.css"));
            //Search 
            bundles.Add(new ScriptBundle("~/bundles/js/search.desktop.js").Include(
                    "~/js/bootstrap.min.js",
                    "~/js/xingfa.common.desktop.js"));

            bundles.Add(new StyleBundle("~/css/search.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                      "~/css/xingfa.common.desktop.css",
                     "~/css/xingfa.search.index.desktop.css"));

            //Category            
            bundles.Add(new ScriptBundle("~/bundles/js/category.desktop.js").Include(
                    "~/js/bootstrap.min.js",
                    "~/js/xingfa.common.desktop.js"));

            bundles.Add(new StyleBundle("~/css/category.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                      "~/css/xingfa.common.desktop.css",
                     "~/css/xingfa.category.index.desktop.css"));

            //Blog            
            bundles.Add(new ScriptBundle("~/bundles/js/blog.index.desktop.js").Include(
                    "~/js/bootstrap.min.js",
                    "~/js/xingfa.common.desktop.js"));

            bundles.Add(new StyleBundle("~/css/blog.index.desktop.css").Include(
                     "~/css/lessbootstrap.css",
                     "~/css/linja.common.desktop.css",
                      "~/css/xingfa.common.desktop.css",
                     "~/css/xingfa.blog.index.desktop.css"));
            #endregion

            #region Mobile

            #endregion




        }
    }
}
