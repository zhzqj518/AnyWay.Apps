using System.Web;
using System.Web.Optimization;

namespace AnyWay.Apps.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/home/index").Include(
                        "~/Scripts/home/index.js"));

            bundles.Add(new StyleBundle("~/content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/content/home/index").Include("~/Content/home/index.css"));

            bundles.Add(new StyleBundle("~/content/themes/default/css").Include(
                       "~/Content/themes/default/easyui.css"));

        }
    }
}
