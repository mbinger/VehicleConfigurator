using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DAL.Common.Booking;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using Microsoft.AspNetCore.Mvc;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    public class OrderEquipmentController : Controller
    {
        public OrderEquipmentController(IRepository<OrderAdditionalEquipmentItem> repository,
            IRepository<Order> orderRepository)
        {
            Repository = repository;
            OrderRepository = orderRepository;
        }

        protected readonly IRepository<OrderAdditionalEquipmentItem> Repository;
        protected readonly IRepository<Order> OrderRepository;


        /// <summary>
        /// Get additional equipment items of order
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<long>> Get(Guid id, int page = 0, int pageSize = 100)
        {
            var query = Repository.SearchFor(p => p.Order.Key == id);
            var items = await Repository.ReadAsync(query);
            return items.Select(p => p.EquipmentId);
        }

        /// <summary>
        /// Set additional equipment of order
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<CudResultDto> Post(Guid id, [FromForm] OrderEquipmentDto dto)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var query = Repository.SearchFor(p => p.Order.Key == id);
                var items = await Repository.ReadAsync(query);
                var deleteTasks = items.Select(p => Repository.DeleteAsync(p.Id)).ToArray();
                Task.WaitAll(deleteTasks);

                var findOrderQuery = OrderRepository.SearchFor(p => p.Key == id);
                var order = await OrderRepository.ReadFirstAsync(findOrderQuery);

                if (dto != null && dto.EquipmentIds != null)
                {
                    foreach (var value in dto.EquipmentIds)
                    {
                        Repository.Create(new OrderAdditionalEquipmentItem
                        {
                            OrderId = order.Id,
                            EquipmentId = value
                        });
                    }
                }
                await Repository.SaveAsync();


                result.Id = id.ToString();
                result.Success = true;
            }
            catch (DalValidationException ex)
            {
                result.Errors = ex.Errors.Select(p => p.Error).ToList();
            }
            return result;
        }
    }
}