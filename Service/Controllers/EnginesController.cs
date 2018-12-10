using AutoMapper;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class EnginesController : BaseApiController<EngineDto, Engine>
    {
        public EnginesController(IMapper mapper, IRepository<Engine> repository) : base(mapper, repository)
        {
        }
    }
}