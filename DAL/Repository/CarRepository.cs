using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Equipment;
using DAL.Context;

namespace DAL.Repository
{
   public class CarRepository : RepositoryBase<Car>
   {
      public CarRepository(ConfigDbContext context) : base(context)
      {
         DbSet = context.Cars;
      }

      public override IQueryable<Car> ReadAll(int? page = null, int? pageSize = null)
      {
         return Paging(DbSet.OrderBy(p => p.Id), page, pageSize);
      }
   }
}
