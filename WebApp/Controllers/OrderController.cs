using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Config;
using WebApp.Const;
using WebApp.Models;

namespace WebApp.Controllers
{
   public class OrderController : Controller
   {
      //
      // GET: /NewOrder/

      public ActionResult Index()
      {
         return Content("");
      }

      private CreateEditOrderModel InitCreateEditModel()
      {
         return new CreateEditOrderModel
         {
            ServiceApiUrl = ConfigurationManager.AppSettings["ServiceApiUrl"]
         };
      }

      public ActionResult Create()
      {
         var model = InitCreateEditModel();
         return View(ViewNames.Order.CreateEdit, model);
      }

      public ActionResult Edit(long id)
      {
         var model = InitCreateEditModel();
         model.Id = id;
         return View(ViewNames.Order.CreateEdit, model);
      }

      public ActionResult Done(long id)
      {
         var model = new OrderCreatedModel
         {
            Id = id
         };
         return View(ViewNames.Order.Done, model);
      }

      public ActionResult Changed(long id)
      {
         var model = new OrderCreatedModel
         {
            Id = id
         };
         return View(ViewNames.Order.Changed, model);
      }
   }
}
