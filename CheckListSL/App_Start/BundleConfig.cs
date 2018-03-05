using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CheckListSL.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app/app.js")
                .Include("~/app/config.js")
                .IncludeDirectory("~/app/components/checklist", "*.js")
                .IncludeDirectory("~/app/components/item", "*.js")
                .IncludeDirectory("~/app/components/translation", "*.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}