using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Common.Interface;
using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        protected RepositoryBase(ConfigDbContext context)
        {
            Context = context;
        }

        protected DbSet<T> DbSet { get; set; }
        protected ConfigDbContext Context { get; set; }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }

        protected virtual IQueryable<T> Query => DbSet;

        /// <summary>
        /// Querly items matching the predicate or null to query all
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null
                ? Query.Where(predicate)
                : Query;
        }

        protected IQueryable<T> Paging<T>(IOrderedQueryable<T> orderedQuery, int page, int pageSize)
        {
            return orderedQuery.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Fetch the prepared query
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="page">Page number or NULL to take all</param>
        /// <param name="pageSize">Page size or NULL to take all</param>
        /// <returns>Fetched result</returns>
        public async Task<List<T>> ReadAsync(IQueryable<T> query, int? page = null, int? pageSize = null)
        {
            if (page != null && pageSize != null)
            {
                var orderedQuery = query as IOrderedQueryable<T>;
                if (orderedQuery == null)
                {
                    //add default ordering
                    orderedQuery = query.OrderBy(p=>p.Id);
                }

                return await Paging(orderedQuery, page.Value, pageSize.Value).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }


        /// <summary>
        /// Read first entity or default
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<T> ReadFirstAsync(IQueryable<T> query)
        {
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Calculate count of the entities defined by the query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns></returns>
        public async Task<long> CountAsync(IQueryable<T> query)
        {
            return await query.LongCountAsync();
        }

        public virtual async Task<T> ReadByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public virtual async Task DeleteAsync(long id)
        {
            var item = await ReadByIdAsync(id);
            DbSet.Remove(item);
        }

        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}