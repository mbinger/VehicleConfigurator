using System;
using System.Collections.Generic;
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
   }
}
