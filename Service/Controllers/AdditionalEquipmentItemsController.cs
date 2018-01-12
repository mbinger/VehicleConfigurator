using AutoMapper;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class AdditionalEquipmentItemsController: BaseApiController<AdditionalEquipmentItemDto, AdditionalEquipmentItem>
    {
        public AdditionalEquipmentItemsController(IMapper mapper, IRepository<AdditionalEquipmentItem> repository) : base(mapper, repository)
        {
        }
    }
}