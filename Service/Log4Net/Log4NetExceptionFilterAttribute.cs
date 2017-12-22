using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using log4net;

namespace Service.Log4Net
{
    /// <summary>
    /// Logger4net integration
    /// </summary>
    public class Log4NetExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var log = LogManager.GetLogger(typeof(WebApiApplication));
            log.Error(context.Exception);
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}