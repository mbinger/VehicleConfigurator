using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp.Const;

namespace WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "DynamicResources_Configuration",
                url: "Scripts/Dynamic/configuration.js",
                defaults: new {controller = ControllerNames.DynamicResources, action=ActionNames.DynamicResources.ConfigurationJs});

            routes.MapRoute(name: "DynamicResources_Plain",
                url: "Scripts/Dynamic/plain.{cultureName}.js",
                defaults:
                    new
                    {
                        controller = ControllerNames.DynamicResources,
                        action = ActionNames.DynamicResources.PlainJs,
                        cultureName = UrlParameter.Optional
                    });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults:
                    new
                    {
                        controller = ControllerNames.Order,
                        action = ActionNames.Order.Create,
                        id = UrlParameter.Optional
                    }
                );
        }
    }
}
