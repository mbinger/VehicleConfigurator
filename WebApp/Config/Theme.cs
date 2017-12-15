using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Config
{
   public class Theme
   {
      public enum ConfiguratorThemeId
      {
         Dark,
         Light
      }

      public ConfiguratorThemeId ThemeId
      {
         get
         {
            if (HttpContext.Current.Session != null)
            {
               var value = HttpContext.Current.Session["ThemeId"];
               if (value != null)
               {
                  return (ConfiguratorThemeId) value;
               }
            }
            return ConfiguratorThemeId.Light;
         }
         set
         {
            if (HttpContext.Current.Session != null)
            {
               HttpContext.Current.Session["ThemeId"] = value;
            }
         }
      }

      public string CssFileName
      {
         get
         {
            switch (ThemeId)
            {
                  case ConfiguratorThemeId.Light:
                  return "~/Content/css/light";

               default:
                  return "~/Content/css/dark";
            }
         }
      }
   }
}