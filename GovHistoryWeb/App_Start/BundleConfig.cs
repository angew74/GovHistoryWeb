using System.Web;
using System.Web.Optimization;

namespace GovHistoryWeb
{
    public class BundleConfig
    {
        // Per ulteriori informazioni sulla creazione di bundle, visitare http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilizzare la versione di sviluppo di Modernizr per eseguire attività di sviluppo e formazione. Successivamente, quando si è
            // pronti per passare alla produzione, utilizzare lo strumento di compilazione disponibile all'indirizzo http://modernizr.com per selezionare solo i test necessari.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/App").Include(
                      "~/Scripts/app/GovStrap.js",
                      "~/Scripts/app/logincontroller.js",
                      "~/Scripts/app/usercontroller.js",
                      "~/Scripts/app/menucontroller.js",
                      "~/Scripts/app/service.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-animate.js",
                    "~/Scripts/angular-messages.js",
                    "~/Scripts/moment.js",
                    "~/Scripts/md-data-table/md-data-table.min.js",
                    "~/Scripts/angular-material.js",
                    "~/Scripts/angular-sanitize.min.js",
                    "~/Scripts/materialize.min.js",
                    "~/Scripts/i18n/angular-locale_it-it.js",
                    "~/Scripts/angular-aria.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    //  "~/Content/bootstrap.css",
                      "~/Content/angular-material.css",
                      "~/Content/angular-layout.css",
                      "~/Content/materialize.min.css",
                      "~/Content/md-data-table/md-data-table.min.css",
                      "~/Content/angular-layout.ie_fixes.css",
                      "~/Content/angular-layout.attributes.css",
                      "~/Content/Site.css"));
        }
    }
}
