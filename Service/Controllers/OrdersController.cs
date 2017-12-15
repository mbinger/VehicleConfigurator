using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Common.Booking;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
   public class OrdersController : BaseApiController<OrderDto, Order>
   {
      public OrdersController(IRepository<Order> repository) : base(repository)
      {
      }
   }
}