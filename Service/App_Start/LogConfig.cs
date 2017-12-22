using System.Web.Http;
using Service.Log4Net;

namespace Service
{
    public class LogConfig
    {
        public static void RegisterLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            GlobalConfiguration.Configuration.Filters.Add(new Log4NetExceptionFilterAttribute());
        }
    }
}