using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
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

        /// <summary>
        /// disable listing all products
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public override Task<IEnumerable<OrderDto>> Get(int page = 0, int pageSize = 100)
        {
            throw new InvalidOperationException();
        }
    }
}