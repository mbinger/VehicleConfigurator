using System.Linq;
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

      public override IQueryable<AdditionalEquipmentItem> ReadAll(int? page = null, int? pageSize = null)
      {
         return Paging(DbSet.OrderBy(p => p.Id), page, pageSize);
      }
   }
}