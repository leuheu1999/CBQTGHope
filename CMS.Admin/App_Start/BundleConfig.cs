using System.Web.Optimization;

namespace CMS.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*"));
            // "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //Bootstrap CSS
            bundles.Add(new StyleBundle("~/Library/bootstrap/css/styles").Include(
                 "~/Library/bootstrap/css/bootstrap.min.css",
                 "~/Library/select2/css/select2.css",
                 "~/Library/bootstrap/css/select2-bootstrap.css",
                  "~/Library/Drop-Down-Combo-Tree/style.css",
                 "~/Library/bootstrap/css/bootstrap-datepicker.min.css",
                 "~/Library/datetimepicker/css/bootstrap-timepicker.min.css",
                 "~/Library/fontawesome/css/all.min.css",
                 "~/Library/bootstrap/css/PNotifyBrightTheme.min.css",
                 "~/Library/bootstrap/css/bootstrap-select.css",
                 "~/Content/PagedList.css",
                 "~/Content/admin.css"
              ));

            bundles.Add(new ScriptBundle("~/Library/bootstrap/js/phone").Include(
               "~/Library/bootstrap/js/html5shiv.min.js",
               "~/Library/bootstrap/js/respond.min.js"
              ));

            //Bootstrap JS
            bundles.Add(new ScriptBundle("~/Library/bootstrap/js/javascripts").Include(
                 //Bootstrap
                "~/Library/jquery-migrate-1.2.1.min.js",
                "~/Library/jquery-ui-1.10.3.custom.min.js",
                "~/Library/popper.min.js",
                "~/Library/select2/js/select2.full.js",
                "~/Library/Drop-Down-Combo-Tree/comboTreePlugin.js",
                "~/Library/bootstrap/js/bootstrap.min.js",
                "~/Library/bootstrap/js/bootstrap-datepicker.min.js",
                "~/Library/datetimepicker/js/bootstrap-timepicker.min.js",
                "~/Library/bootstrap/js/PNotify.min.js",
                "~/Library/bootstrap/js/bootstrap-select.js",
                "~/Library/bootstrap/js/bootstrap-notify.min.js",
                "~/Library/tinymce/tinymce.min.js",
                "~/Library/BootstrapMenu.min.js",
                "~/Library/validator.min.js",
                "~/Scripts/common.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/jquery/Eform").Include(
                      "~/Scripts/jquery-3.2.1.min.js"));
            bundles.Add(new StyleBundle("~/Library/bootstrap/css/Eform").Include(
                    "~/Library/bootstrap/css/bootstrap.min.css",
                    "~/Library/bootstrap/css/bootstrap-datepicker.min.css",
                    "~/Library/bootstrap/css/PNotifyBrightTheme.min.css",
                    "~/Library/jquery-ui-1.12.1.custom/jquery-ui.min.css",
                    "~/Library/fontawesome/css/all.min.css",
                    "~/Library/codemirror/codemirror.css",
                    "~/Content/eform.css"
          ));

            //Bootstrap JS
            bundles.Add(new ScriptBundle("~/Library/bootstrap/js/Eform").Include(
                //Bootstrap
                "~/Library/popper.min.js",
                "~/Library/jquery-ui-1.12.1.custom/jquery-ui.min.js",
                "~/Library/bootstrap/js/bootstrap.min.js",
                "~/Library/bootstrap/js/PNotify.min.js",
                "~/Library/bootstrap/js/bootstrap-datepicker.min.js",
                "~/Library/bootstrap/js/bootstrap-notify.min.js",
                 "~/Library/codemirror/codemirror.js",
                "~/Scripts/common.js"
               ));


        }
    }
}
