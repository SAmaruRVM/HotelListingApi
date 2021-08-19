using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelListing.WebAPI.IRepository
{
    public interface IGenericRepository<in TKey, TEntity> where TKey : struct where TEntity : class, new()
    {
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             params string[] includeExpressions);

        Task<TEntity> GetSingleAsync<TProperty>(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, TProperty>>[] includeExpressions);


        void InsertSingle(TEntity entity);
        void InsertMultiple(IEnumerable<TEntity> entities);

        Task DeleteSingleByPrimaryKeyAsync(TKey primaryKey);
        Task DeleteMultipleByPrimaryKeyAsync(IEnumerable<TKey> keys);

        void DeleteSingle(TEntity entity);
        void DeleteMultiple(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);
    }
}
