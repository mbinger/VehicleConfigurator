using System.Web.Mvc;
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

        public ActionResult Create()
        {
            var model = new CreateEditOrderModel();
            return View(ViewNames.Order.CreateEdit, model);
        }

        public ActionResult Edit(string id)
        {
            var model = new CreateEditOrderModel
            {
                Id = id
            };
            return View(ViewNames.Order.CreateEdit, model);
        }

        public ActionResult Done(string id)
        {
            var model = new OrderCreatedModel
            {
                Id = id
            };
            return View(ViewNames.Order.Done, model);
        }

        public ActionResult Changed(string id)
        {
            var model = new OrderCreatedModel
            {
                Id = id
            };
            return View(ViewNames.Order.Changed, model);
        }
    }
}