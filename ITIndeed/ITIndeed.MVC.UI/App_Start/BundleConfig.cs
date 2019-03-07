using System.Web;
using System.Web.Optimization;

namespace ITIndeed.MVC.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Theme/css").Include(
                      "~/Content/Theme/css/bootstrap.min.css",
                      "~/Content/Theme/css/font-awesome.min.css",
                      "~/Content/Theme/css/animate.css",
                      "~/Content/Theme/css/style.css"));

            bundles.Add(new ScriptBundle("~/Content/Theme/js").Include(
                      "~/Theme/js/jquery-2.1.1.min.js",
                      "~/Theme/js/bootstrap.min.js",
                      "~/Theme/js/jquery.prettyPhoto.js",
                      "~/Theme/js/jquery.isotope.min.js",
                      "~/Theme/js/wow.min.js",
                      "~/Theme/js/functions.js"));
        }
    }
}
