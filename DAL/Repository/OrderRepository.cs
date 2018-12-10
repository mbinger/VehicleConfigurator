using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Common.Booking;
using DAL.Common.Reference;
using DAL.Context;

namespace DAL.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(ConfigDbContext context) : base(context)
        {
            DbSet = context.Orders;
        }

        public override void Update(Order entity)
        {
            entity.DateChangedUtc = DateTime.UtcNow;
            entity.StatusId = (long) OrderStatusId.Changed;
            base.Update(entity);
        }

        public override void Create(Order entity)
        {
            entity.DateCreatedUtc = DateTime.UtcNow;
            entity.DateChangedUtc = null;
            entity.StatusId = (long) OrderStatusId.New;

            var generateUniqueKey = true;
            while (generateUniqueKey)
            {
                entity.Key = Guid.NewGuid();
                generateUniqueKey = Context.Orders.Any(p => p.Key == entity.Key);
            }

            base.Create(entity);
        }
    }
}