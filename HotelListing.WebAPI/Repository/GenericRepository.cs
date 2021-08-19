using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.IRepository;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace HotelListing.WebAPI.Repository
{
    public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TKey : struct where TEntity : class, new()
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _entity;
        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public void DeleteMultiple(IEnumerable<TEntity> entities) => _context.RemoveRange(entities);

        public async Task DeleteMultipleByPrimaryKeyAsync(IEnumerable<TKey> keys)
        {
            List<TEntity> entitiesToDelete = new();

            foreach (TKey key in keys)
            {
                entitiesToDelete.Add(await _entity.FindAsync(key));
            }

            _context.RemoveRange(entitiesToDelete);
        }

        public void DeleteSingle(TEntity entity) => _context.Remove(entity);

        public async Task DeleteSingleByPrimaryKeyAsync(TKey primaryKey) => _context.Remove(await _entity.FindAsync(primaryKey));

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>
            expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params string[] includeExpressions)
        {
            IQueryable<TEntity> query = _entity.AsNoTracking();

            if (includeExpressions is not null && includeExpressions.Any())
            {
                foreach (var include in includeExpressions)
                    query = query.Include(include);
            }

            if (expression is not null)
                query = query.Where(expression);


            if (orderBy is not null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync<TProperty>(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, TProperty>>[] includeExpressions)
        {
            IQueryable<TEntity> query = _entity;

            if (includeExpressions is not null && includeExpressions.Any())
            {
                foreach (var include in includeExpressions)
                    query = query.Include(include);
            }


            return await query.AsNoTracking()
                              .FirstOrDefaultAsync(expression);
        }

        public void InsertMultiple(IEnumerable<TEntity> entities) => _context.AddRange(entities);

        public void InsertSingle(TEntity entity) => _context.Add(entity);

        public void Update(TEntity entityToUpdate)
        {
            _context.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
