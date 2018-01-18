using System.Linq;
using DAL.Common.Equipment;
using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
   public class EngineRepository : RepositoryBase<Engine>
   {
      public EngineRepository(ConfigDbContext context) : base(context)
      {
         DbSet = context.Engines;
      }

      public override IQueryable<Engine> ReadAll(int? page = null, int? pageSize = null)
      {
         return Paging(DbSet.Include(p=>p.FuelType).OrderBy(p => p.Id), page, pageSize);
      }
   }
}