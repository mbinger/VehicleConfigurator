using DAL.Common.Equipment;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repository
{
    public class ColorRepository : RepositoryBase<Color>
    {
        public ColorRepository(ConfigDbContext context) : base(context)
        {
            DbSet = context.Colors;
        }

        protected override IQueryable<Color> Query =>
            Context.Colors.Include(p => p.Type);
    }
}
