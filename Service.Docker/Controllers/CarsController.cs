using AutoMapper;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class CarsController : BaseApiController<CarDto, Car>
    {
        public CarsController(IMapper mapper, IRepository<Car> repository) : base(mapper, repository)
        {
        }
    }
}