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

        protected override IQueryable<Rim> Query =>
            Context.Rims.Include(p => p.Type);
    }
}