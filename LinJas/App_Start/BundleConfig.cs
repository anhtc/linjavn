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
            bundles.Add(new ScriptBundle("~/bundles/desktop-home").Include(
                     "~/Asset/js/jquery-1.10.2.min.js",
                     "~/Asset/js/bootstrap.min.js",
                     "~/Asset/js/lazysizes.min.js",
                     "~/Asset/js/common.desktop.js",
                     "~/Asset/js/home.index.desktop.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/desktop/css-home").Include(
                   "~/Asset/css/bootstrap.css",
                   "~/Asset/css/common.desktop.css",
                   "~/Asset/css/home.index.desktop.css"
                   ));

            //giới thiệu
            bundles.Add(new ScriptBundle("~/bundles/desktop-gioithieu").Include(
                    "~/Asset/js/jquery-1.10.2.min.js",
                    "~/Asset/js/bootstrap.min.js",
                    "~/Asset/js/lazysizes.min.js",
                    "~/Asset/js/common.desktop.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/desktop/css-gioithieu").Include(
                   "~/Asset/css/bootstrap.css",
                   "~/Asset/css/common.desktop.css",
                   "~/Asset/css/gioithieu.index.desktop.css"
                   ));

            //Liên hệ
            bundles.Add(new ScriptBundle("~/bundles/desktop-lienhe").Include(
                   "~/Asset/js/jquery-1.10.2.min.js",
                   "~/Asset/js/bootstrap.min.js",
                   "~/Asset/js/lazysizes.min.js",
                   "~/Asset/js/common.desktop.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/desktop/css-lienhe").Include(
                   "~/Asset/css/bootstrap.css",
                   "~/Asset/css/common.desktop.css",
                   "~/Asset/css/lienhe.index.desktop.css"
                   ));

            #endregion

            #region Mobile

            #endregion




        }
    }
}
