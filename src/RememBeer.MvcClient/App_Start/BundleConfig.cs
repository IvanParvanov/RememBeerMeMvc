using System.Diagnostics.CodeAnalysis;
using System.Web.Optimization;

namespace RememBeer.MvcClient
{
    [ExcludeFromCodeCoverage]
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                                                                     "~/Scripts/jquery-{version}.js",
                                                                     "~/Scripts/jquery.unobtrusive-ajax.min.js",
                                                                     "~/Scripts/jquery.signalR-2.2.1.min.js",
                                                                     "~/Scripts/jquery.validate*",
                                                                     "~/Scripts/materialize.min.js",
                                                                     "~/Scripts/respond.js"
                                                                    ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                                                                     "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                                                 "~/Content/materialize-v0.97.5/css/materialize.min.css",
                                                                 "~/Content/font-awesome.min.css",
                                                                 "~/Content/site.css"));
        }
    }
}
