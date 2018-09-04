using System.Web;
using System.Web.Optimization;

namespace Attendance_System
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/html5-qrcode.min.js",
                      "~/Scripts/datatables.js",
                      "~/Scripts/js/all.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/lumen.bootstrap.min.css",
                      "~/Content/css/all.css",
                      "~/Content/site.css",
                      "~/Content/boon-all.css",
                      "~/Content/datatables.css"
                      ));
        }
    }
}
