using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common.Interface
{
    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Create item
        /// </summary>
        /// <returns></returns>
        void Create(TEntity entity);

        /// <summary>
        /// Querly items matching the predicate or null to query all
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Fetch the prepared query
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="page">Page number or NULL to take all</param>
        /// <param name="pageSize">Page size or NULL to take all</param>
        /// <returns>Fetched result</returns>
        Task<List<TEntity>> ReadAsync(IQueryable<TEntity> query, int? page = null, int? pageSize = null);

        /// <summary>
        /// Read first entity or default
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TEntity> ReadFirstAsync(IQueryable<TEntity> query);

        /// <summary>
        /// Calculate count of the entities defined by the query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns></returns>
        Task<long> CountAsync(IQueryable<TEntity> query);

        /// <summary>
        /// Read item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> ReadByIdAsync(long id);

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(long id);

        /// <summary>
        /// Save changes
        /// </summary>
        Task SaveAsync();
    }
}