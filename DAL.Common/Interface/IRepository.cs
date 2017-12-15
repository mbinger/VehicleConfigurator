using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Read all items
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> ReadAll(int? page = null, int? pageSize = null);

        /// <summary>
        /// Fetch the prepared query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>Fetched result</returns>
        Task<List<TEntity>> FetchAsync(IQueryable<TEntity> query);

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