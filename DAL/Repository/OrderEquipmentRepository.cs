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
   }
}
