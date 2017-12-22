using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApp.Modules
{
    public class CookieLocalizationModule: IHttpModule
    {
        public const string CookieName = "lang";

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            // eat the cookie (if any) and set the culture
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
                string lang = cookie.Value;
                var culture = new System.Globalization.CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}