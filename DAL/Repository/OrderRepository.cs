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

        public override IQueryable<Order> ReadAll(int? page = null, int? pageSize = null)
        {
            return Paging(DbSet.OrderBy(p => p.Id), page, pageSize);
        }

        public override void Update(Order entity)
        {
            entity.DateChangedUtc = DateTime.UtcNow;
            entity.StatusId = (long) OrderStatusId.Changed;
            base.Update(entity);
        }

        public override void Create(Order entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DateCreatedUtc = DateTime.UtcNow;
            entity.DateChangedUtc = null;
            entity.StatusId = (long) OrderStatusId.New;
            base.Create(entity);
        }

        public override async Task<Order> ReadByIdAsync(string id)
        {
            var guidId = Guid.Parse(id);
            return await DbSet.FindAsync(guidId);
        }
    }
}