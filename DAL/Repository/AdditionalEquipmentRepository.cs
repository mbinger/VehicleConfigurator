using DAL.Common.Equipment;
using DAL.Context;

namespace DAL.Repository
{
   public class AdditionalEquipmentRepository : RepositoryBase<AdditionalEquipmentItem>
   {
      public AdditionalEquipmentRepository(ConfigDbContext context) : base(context)
      {
         DbSet = context.AdditionalEquipmentItems;
      }
   }
}