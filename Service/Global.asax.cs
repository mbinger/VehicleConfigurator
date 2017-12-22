using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Diagnostics;
using log4net;

namespace Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            LogConfig.RegisterLogging();
            DependenciesConfig.RegisterDependencies();
            MappingConfig.RegisterMapping();
            
            var log = LogManager.GetLogger(typeof(WebApiApplication));
            log.Info("Applicaton startet");
        }
    }
}
