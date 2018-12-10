using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Common.Booking;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
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

        /// <summary>
        /// disable default get by long id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NonAction]
        public override Task<OrderDto> Get(long id)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// get by key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<OrderDto> GetByKey(Guid id)
        {
            var query = Repository.SearchFor(p => p.Key == id);
            var item = await Repository.ReadFirstAsync(query);
            return Mapper.Map<OrderDto>(item);
        }

        // POST api/colors
        [HttpPost]
        public override async Task<CudResultDto> Post([FromForm] OrderDto value)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var entity = Mapper.Map<Order>(value);
                Repository.Create(entity);
                await Repository.SaveAsync();

                result.Id = entity.Key.ToString();
                result.Success = true;
            }
            catch (DalValidationException ex)
            {
                result.Errors = ex.Errors.Select(p => p.Error).ToList();
            }

            return result;
        }

        /// <summary>
        /// disable default put by long id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [NonAction]
        public override Task<CudResultDto> Put(long id, [FromForm] OrderDto value)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Put order by key
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name ="Put")]
        public async Task<CudResultDto> PutByKey(Guid id, [FromForm] OrderDto value)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var getOrderQuery = Repository.SearchFor(p => p.Key == id);
                var entity = await Repository.ReadFirstAsync(getOrderQuery);
                Mapper.Map(value, entity);
                Repository.Update(entity);
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