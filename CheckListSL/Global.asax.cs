using CheckListSL.App_Start;
using CheckListSL.DAL;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CheckListSL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configuration
               .Formatters
               .JsonFormatter
               .SerializerSettings
               .ContractResolver = new CamelCasePropertyNamesContractResolver();
            AreaRegistration.RegisterAllAreas();
            //Every time when the models change DB is dropped
            Database.SetInitializer<ChecklistSLContext>(new DropCreateDatabaseIfModelChanges<ChecklistSLContext>());
            // Pass a delegate to the Configure method. To make rout attributes work
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
