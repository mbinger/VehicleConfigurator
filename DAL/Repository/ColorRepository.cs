﻿using System.Linq;
using DAL.Common.Equipment;
using DAL.Context;

namespace DAL.Repository
{
   public class ColorRepository : RepositoryBase<Color>
   {
      public ColorRepository(ConfigDbContext context):base(context)
      {
         DbSet = context.Colors;
      }

      public override IQueryable<Color> ReadAll(int? page = null, int? pageSize = null)
      {
         return Paging(DbSet.OrderBy(p => p.Id), page, pageSize);
      }
   }
}
