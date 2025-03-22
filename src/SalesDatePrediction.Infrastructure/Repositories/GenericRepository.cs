using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Add entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Add entity to memory
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Add range of entities to memory
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Delete entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity from memory
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Delete range of entities from memory
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(IReadOnlyList<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Get all entities from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Get entities from the database based on the predicate, order by and include string
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeString"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeString, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get entities list
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        //public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (disableTracking) query = query.AsNoTracking();

        //    if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null)
        //        return await orderBy(query).ToListAsync();

        //    return await query.ToListAsync();
        //}



        public async Task<IReadOnlyList<TResult>> GetAsync<TResult>(
             Expression<Func<T, TResult>> selector,
             Expression<Func<T, bool>>? predicate = null,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
             bool disableTracking = true)
                {
                    IQueryable<T> query = _dbSet;

                    if (disableTracking)
                        query = query.AsNoTracking();

                    if (predicate != null)
                        query = query.Where(predicate);

                    if (orderBy != null)
                        query = orderBy(query);

            return await query.Select(selector).ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAsync(
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           bool disableTracking = true,
           params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }



        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return (await _dbSet.FindAsync(id))!;
        }

        /// <summary>
        /// Get entity based on the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);


            return (await query.FirstOrDefaultAsync())!;
        }




        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;

        }

        /// <summary>
        /// Update entity in memory
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateEntity(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
