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

        protected override IQueryable<Engine> Query => 
            Context.Engines.Include(p=>p.FuelType);
    }
}