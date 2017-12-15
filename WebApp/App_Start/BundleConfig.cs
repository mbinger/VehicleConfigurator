using System.Web;
using System.Web.Optimization;

namespace WebApp
{
   public class BundleConfig
   {
      // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
      public static void RegisterBundles(BundleCollection bundles)
      {
         bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

         bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*"));

         bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-*"));

         bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   "~/Scripts/bootstrap.js",
                   "~/Scripts/respond.js"));

         bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                    "~/Scripts/knockout-{version}.js"));


         bundles.Add(new ScriptBundle("~/bundles/configurator").Include(
                   "~/Scripts/service-client.js"));

         bundles.Add(new StyleBundle("~/Content/css/dark").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/bootstrap-dark.css",
                   "~/Content/configurator-dark.css"));

         bundles.Add(new StyleBundle("~/Content/css/light").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/bootstrap-theme.css",
                   "~/Content/configurator.css"));
      }
   }
}