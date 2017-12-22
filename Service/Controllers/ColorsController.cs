using DAL.Common.Equipment;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class ColorsController : BaseApiController<ColorDto, Color>
    {
        public ColorsController(IRepository<Color> repository) : base(repository)
        {
        }
    }
}