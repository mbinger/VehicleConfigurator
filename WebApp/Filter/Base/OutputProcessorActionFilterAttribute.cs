using System.Web.Mvc;

namespace WebApp.Filter.Base
{
    /// <summary>
    /// Filter attribute with output post-procession
    /// </summary>
    public abstract class OutputProcessorActionFilterAttribute: ActionFilterAttribute
    {
        protected OutputProcessorActionFilterAttribute(IOutputProcessor processor)
        {
            _processor = processor;
        }

        private readonly IOutputProcessor _processor;

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.Filter = new OutputProcessorStream(response.Filter, _processor, filterContext.HttpContext.Response.ContentEncoding);
            base.OnResultExecuted(filterContext);
        }
    }
}