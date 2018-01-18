using System;
using System.Collections.Generic;
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
        public OrderEquipmentController(IRepository<OrderAdditionalEquipmentItem> repository)
        {
            _repository = repository;
        }

        private IRepository<OrderAdditionalEquipmentItem> _repository;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                var disposableRepository = _repository as IDisposable;
                disposableRepository?.Dispose();
                _repository = null;
            }
        }

        /// <summary>
        /// Get additional equipment items of order
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IEnumerable<long> Get(string id, int page = 0, int pageSize = 100)
        {
            var guid = Guid.Parse(id);
            return _repository.ReadAll(page, pageSize)
                .Where(p => p.OrderId == guid)
                .AsEnumerable()
                .Select(p => p.EquipmentId);
        }

        /// <summary>
        /// Set additional equipment of order
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<CudResultDto> Post(string id, [FromForm] OrderEquipmentDto dto)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var guid = Guid.Parse(id);
                var query = _repository.ReadAll().Where(p => p.OrderId == guid);
                var items = await _repository.FetchAsync(query);
                var deleteTasks = items.Select(p => _repository.DeleteAsync(p.GetGenericId())).ToArray();
                Task.WaitAll(deleteTasks);

                if (dto != null && dto.EquipmentIds != null)
                {
                    foreach (var value in dto.EquipmentIds)
                    {
                        _repository.Create(new OrderAdditionalEquipmentItem
                        {
                            OrderId = guid,
                            EquipmentId = value
                        });
                    }
                }
                await _repository.SaveAsync();


                result.Id = id;
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