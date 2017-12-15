using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using DAL.Common.Booking;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class OrderEquipmentController : ApiController
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
                if (disposableRepository != null)
                {
                    disposableRepository.Dispose();
                }
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
        public IEnumerable<long> Get(long id, int page = 0, int pageSize = 100)
        {
            return _repository.ReadAll(page, pageSize)
               .Where(p => p.OrderId == id)
               .AsEnumerable()
               .Select(p => p.EquipmentId);
        }

        /// <summary>
        /// Set additional equipment of order
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<CudResultDto> Post(long id, [FromBody] OrderEquipmentDto dto)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var query = _repository.ReadAll().Where(p => p.OrderId == id);
                var items = await _repository.FetchAsync(query);
                var deleteTasks = items.Select(p => _repository.DeleteAsync(p.Id)).ToArray();
                Task.WaitAll(deleteTasks);

                if (dto != null && dto.EquipmentIds != null)
                {
                    foreach (var value in dto.EquipmentIds)
                    {
                        _repository.Create(new OrderAdditionalEquipmentItem
                        {
                            OrderId = id,
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
