using System.Web.Optimization;

namespace BlueSignal
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
                 "~/Scripts/moment.js",
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/bootstrap-datepicker.min.js",
                       "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                         //"~/Content/bootstrap-datepicker.min.css",
                         "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/pages.css",
                      "~/Content/Site.css"));




            #region MyCustomPackages


            bundles.Add(new ScriptBundle("~/bundles/common").Include(
             "~/Scripts/Common/Index.js",
             "~/Scripts/Common/GWACommon.js",
             "~/Scripts/Common/CommonEnums.js"
               ));


            bundles.Add(new ScriptBundle("~/bundles/Products").Include(
             "~/Scripts/Common/ProductCountryFeatureMapping/ProductCountryFeatureMappingJs.js"
               ));


            bundles.Add(new ScriptBundle("~/bundles/Customer").Include(
             "~/Scripts/Common/Customer/CustomerDetailJs.js"
               ));


            #endregion MyCustomPackages

        }
    }
}
