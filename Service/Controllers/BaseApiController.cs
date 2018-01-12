using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using Microsoft.AspNetCore.Mvc;
using Service.Common.DataTransfer;
using Service.Common.Interface;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseApiController<TDto, TEntity> : Controller
       where TEntity : IEntity, new()
       where TDto : IDataTransferObject, new()
    {
        protected BaseApiController(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private IMapper _mapper;
        private IRepository<TEntity> _repository;

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

        // GET api/colors
        [HttpGet]
        public virtual async Task<IEnumerable<TDto>> Get(int page = 0, int pageSize = 100)
        {
            var query = _repository.ReadAll(page, pageSize);
            var fetched = await _repository.FetchAsync(query);
            return fetched.Select(p => _mapper.Map<TDto>(p));
        }

        // GET api/colors/5
        [HttpGet("{id}")]
        public virtual async Task<TDto> Get(string id)
        {
            var entity = await _repository.ReadByIdAsync(id);
            var dto = _mapper.Map<TDto>(entity);
            return dto;
        }

        // POST api/colors
        [HttpPost]
        public async Task<CudResultDto> Post([FromBody] TDto value)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var entity = _mapper.Map<TEntity>(value);
                _repository.Create(entity);
                await _repository.SaveAsync();

                result.Id = entity.GetGenericId();
                result.Success = true;
            }
            catch (DalValidationException ex)
            {
                result.Errors = ex.Errors.Select(p => p.Error).ToList();
            }

            return result;
        }

        // PUT api/colors/5
        [HttpPut("{id}")]
        public async Task<CudResultDto> Put(string id, [FromBody] TDto value)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var entity = await _repository.ReadByIdAsync(id);
                _mapper.Map(value, entity);
                _repository.Update(entity);
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

        // DELETE api/colors/5
        [HttpDelete("{id}")]
        public async Task<CudResultDto> Delete(string id)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                await _repository.DeleteAsync(id);
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