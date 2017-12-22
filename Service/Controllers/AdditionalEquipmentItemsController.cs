using DAL.Common.Equipment;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
    public class AdditionalEquipmentItemsController: BaseApiController<AdditionalEquipmentItemDto, AdditionalEquipmentItem>
    {
        public AdditionalEquipmentItemsController(IRepository<AdditionalEquipmentItem> repository) : base(repository)
        {
        }
    }
}