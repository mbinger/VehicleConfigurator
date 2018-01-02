using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebApp.Filter.Base;

namespace WebApp.Filter
{
    /// <summary>
    /// Cut script tags to render dynamic javascript with Razor partial views
    /// </summary>
    public class RazorJavaScriptFilterAttribute : OutputProcessorActionFilterAttribute
    {
        public RazorJavaScriptFilterAttribute():base(new CutScriptTagOutputProcessor())
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.ContentType = "text/javascript";
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// Remove script tag from output
        /// </summary>
        private class CutScriptTagOutputProcessor : IOutputProcessor
        {
            public string Process(string input)
            {
                var scriptRegex = new Regex(@"<\/*script.*>");
                var result = scriptRegex.Replace(input, "").Trim();
                return result;
            }
        }
    }
}