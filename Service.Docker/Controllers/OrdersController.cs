using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Common.Booking;
using DAL.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class OrdersController : BaseApiController<OrderDto, Order>
    {
        public OrdersController(IMapper mapper, IRepository<Order> repository) : base(mapper, repository)
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