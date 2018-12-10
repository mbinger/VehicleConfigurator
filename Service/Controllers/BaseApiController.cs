using System;
using System.Collections.Generic;
using System.Globalization;
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
            Mapper = mapper;
            Repository = repository;
        }

        protected IMapper Mapper;
        protected IRepository<TEntity> Repository;

        // GET api/colors
        [HttpGet]
        public virtual async Task<IEnumerable<TDto>> Get(int page = 0, int pageSize = 100)
        {
            var query = Repository.SearchFor();
            var fetched = await Repository.ReadAsync(query, page, pageSize);
            return fetched.Select(p => Mapper.Map<TDto>(p));
        }

        // GET api/colors/5
        [HttpGet("{id}")]
        public virtual async Task<TDto> Get(long id)
        {
            var entity = await Repository.ReadByIdAsync(id);
            var dto = Mapper.Map<TDto>(entity);
            return dto;
        }

        // POST api/colors
        [HttpPost]
        public virtual async Task<CudResultDto> Post([FromForm] TDto value)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var entity = Mapper.Map<TEntity>(value);
                Repository.Create(entity);
                await Repository.SaveAsync();

                result.Id = entity.Id.ToString(CultureInfo.InvariantCulture);
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
        public virtual async Task<CudResultDto> Put(long id, [FromForm] TDto value)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                var entity = await Repository.ReadByIdAsync(id);
                Mapper.Map(value, entity);
                Repository.Update(entity);
                await Repository.SaveAsync();

                result.Id = id.ToString(CultureInfo.InvariantCulture);
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
        public virtual async Task<CudResultDto> Delete(long id)
        {
            var result = new CudResultDto
            {
                Success = false
            };

            try
            {
                await Repository.DeleteAsync(id);
                await Repository.SaveAsync();

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