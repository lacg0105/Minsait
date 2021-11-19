using System.Web;
using System.Web.Optimization;

namespace Minsait_MVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/animate.css",
                      "~/Content/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                "~/Scripts/plugins/inspinia/Navigation.js",
                 "~/Scripts/plugins/inspinia/inspinia.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/plugins/inspinia/skin.config.min.js"));

            // KENDO styles
            bundles.Add(new StyleBundle("~/Content/plugins/Kendo/KendoUi").Include(
                  "~/Content/plugins/Kendo2019/kendo.common.min.css",
                   "~/Content/plugins/Kendo2019/kendo.default.min.css",
                      "~/Content/plugins/Kendo2019/kendo.default.mobile.min.css",
                       "~/Content/plugins/Kendo2019/kendo.common-material.min.css",
                        "~/Content/plugins/Kendo2019/kendo.material.min.css",
                         "~/Content/plugins/Kendo2019/kendo.material.mobile.min.css",
                      "~/Content/KendoMinsait.css"));

            // Kendo ui 
            bundles.Add(new ScriptBundle("~/plugins/Kendo").Include(
                      "~/Scripts/plugins/Kendo2019/kendo.all.min.js",
                       "~/Scripts/plugins/Kendo2019/jszip.min.js",
                        "~/Scripts/plugins/Kendo2019/cultures/kendo.culture.es-ES.min.js",
                       "~/Scripts/plugins/Kendo2019/messages/kendo.messages.es-ES.min.js"

                      ));

            // Sweet alert Styless
            bundles.Add(new StyleBundle("~/plugins/sweetAlertStyles").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));

            // Sweet alert
            bundles.Add(new ScriptBundle("~/plugins/sweetAlert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

        }
    }
}
