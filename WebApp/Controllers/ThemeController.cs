using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Config;
using WebApp.Const;

namespace WebApp.Controllers
{
    public class ThemeController : Controller
    {
       public ActionResult Index(string id)
       {
          try
          {
             new Config.Theme().ThemeId = (Theme.ConfiguratorThemeId)Enum.Parse(typeof(Config.Theme.ConfiguratorThemeId), id);
          }
          catch (Exception ex)
          {
             //ignore
          }
          return RedirectToAction(ActionNames.Order.Create, ControllerNames.Order);
       }
    }
}
