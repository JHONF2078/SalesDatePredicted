﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Common.Interfaces
{
     public interface IRepository<T> where T : class
     {

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
                                       string? includeString,
                                       bool disableTracking = true);
        //Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
        //                               Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        //                               List<Expression<Func<T, object>>>? includes = null,
        //                               bool disableTracking = true);


        Task<IReadOnlyList<T>> GetAsync(
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           bool disableTracking = true,
           params Expression<Func<T, object>>[] includes);


        Task<IReadOnlyList<TResult>> GetAsync<TResult>(
           Expression<Func<T, TResult>> selector,
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           bool disableTracking = true);



        Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate,
                                         List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        void AddRange(List<T> entities);
        void DeleteRange(IReadOnlyList<T> entities);
    }
}
