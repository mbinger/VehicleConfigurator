using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Booking;
using DAL.Context;

namespace DAL.Repository
{
   public class OrderEquipmentRepository: RepositoryBase<OrderAdditionalEquipmentItem>
   {
      public OrderEquipmentRepository(ConfigDbContext context): base(context)
      {
         DbSet = context.OrderAdditionalEquipmentItems;
      }

      public override IQueryable<OrderAdditionalEquipmentItem> ReadAll(int? page = null, int? pageSize = null)
      {
         return Paging(DbSet.OrderBy(p => p.Id), page, pageSize);
      }
   }
}
