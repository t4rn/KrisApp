using System.Collections.Generic;
using System.Web.Optimization;

namespace KrisApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            #region Scripts

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
                "~/Scripts/respond.js"));

            var appBundle = new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/_analytics.js",
                "~/Scripts/globalize.js",
                "~/Scripts/jquery.validate.globalize.js"
                );
            appBundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(appBundle);

            bundles.Add(new ScriptBundle("~/bundles/redactor").Include(
                "~/Scripts/redactor/redactor.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/sunlight").Include(
                "~/Scripts/sunlight/sunlight-min.js",
                "~/Scripts/sunlight/sunlight.csharp-min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/calc").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/app/kris-app.js",
                "~/app/calc/calcController.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/_autocomplete.js"
                ));

            #endregion

            #region Styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                //"~/Content/bootstrap.css",
                "~/Content/bootswatch/cosmo/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/wysiwyg").Include(
                "~/Content/redactor/redactor.css"));

            bundles.Add(new StyleBundle("~/Content/highlighter").Include(
                "~/Content/sunlight/sunlight.default.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                "~/Content/themes/base/jquery-ui.css"));

            #endregion
        }
    }

    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
