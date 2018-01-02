using System.Web.Http;
using log4net;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Service.Startup))]
namespace Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
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
