using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Common.Interface;
using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T>, IDisposable where T : class, IEntity
    {
        protected RepositoryBase(ConfigDbContext context)
        {
            Context = context;
        }

        protected DbSet<T> DbSet { get; set; }
        protected ConfigDbContext Context { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public abstract IQueryable<T> ReadAll(int? page = null, int? pageSize = null);

        protected IQueryable<TEntity> Paging<TEntity>(IOrderedQueryable<TEntity> orderedQuery, int? page, int? pageSize)
        {
            if (page != null && pageSize != null)
            {
                return orderedQuery.Skip(page.Value * pageSize.Value).Take(pageSize.Value);
            }
            return orderedQuery;
        }

        public async Task<List<T>> FetchAsync(IQueryable<T> query)
        {
            return await query.ToListAsync<T>();
        }

        public virtual async Task<T> ReadByIdAsync(string id)
        {
            var longId = long.Parse(id);
            return await DbSet.FindAsync(longId);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public virtual async Task DeleteAsync(string id)
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