using System.Linq;
using DAL.Common.Equipment;
using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
   public class RimRepository : RepositoryBase<Rim>
   {
      public RimRepository(ConfigDbContext context) : base(context)
      {
         DbSet = context.Rims;
      }

      public override IQueryable<Rim> ReadAll(int? page = null, int? pageSize = null)
      {
         return Paging(DbSet.Include(p=>p.Type).OrderBy(p => p.Id), page, pageSize);
      }
   }
}