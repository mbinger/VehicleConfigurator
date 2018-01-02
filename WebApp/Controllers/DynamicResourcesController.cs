using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Const;
using WebApp.Filter;
using WebApp.Resource;

namespace WebApp.Controllers
{
    public class DynamicResourcesController : Controller
    {
        /// <summary>
        /// Use Scripts/configuration.js
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, RazorJavaScriptFilter, OutputCache(Duration = 3600)]
        public ActionResult ConfigurationJs()
        {
            return PartialView(ViewNames.DynamicResources.ConfigurationJs);
        }

        /// <summary>
        /// Use Scripts/plain.{culture}.js
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, RazorJavaScriptFilter, OutputCache(Duration = 3600, VaryByParam = "cultureName")]
        public ActionResult PlainJs(string cultureName)
        {
            try
            {
                var culture = !String.IsNullOrEmpty(cultureName)
                    ? CultureInfo.GetCultureInfo(cultureName)
                    : CultureInfo.DefaultThreadCurrentUICulture;

                var resourceSet = Plain.ResourceManager.GetResourceSet(culture, true, true);
                if (resourceSet != null)
                {
                    var model = new Dictionary<string, string>();
                    foreach (DictionaryEntry entry in resourceSet)
                    {
                        var key = entry.Key.ToString();
                        var value = HttpUtility.JavaScriptStringEncode(entry.Value.ToString());
                        model[key] = value;
                    }
                    
                    return PartialView(ViewNames.DynamicResources.PlainJs, model);
                }
            }
            catch
            {
                //ignore
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}