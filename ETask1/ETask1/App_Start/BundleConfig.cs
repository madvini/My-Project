using System.Web;
using System.Web.Optimization;

namespace ETask1
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/menu-styles.css",
                        "~/Content/css/jquery.datepick.css",
                        "~/Content/css/style.css"));
        }
    }
}