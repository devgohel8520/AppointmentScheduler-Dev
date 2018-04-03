using System.Web;
using System.Web.Optimization;

namespace App.Schedule.Web.Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //Metis Menu
            bundles.Add(new ScriptBundle("~/bundles/metismenu").Include(
                      "~/Scripts/metisMenu.js",
                      "~/Scripts/vendor/js/sb-admin-2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Scripts/vendor/css/sb-admin-2.css",
                      "~/Content/vendor/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}
