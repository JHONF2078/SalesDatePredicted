using Microsoft.Extensions.DependencyInjection;
using SalesDatePrediction.Application.Common.Interfaces;
using SalesDatePrediction.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Infrastructure.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        //store in memory all repositories are instantiated
        private Hashtable? _repositories;
        private readonly AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException ex)
            {
                throw new Exception("Error in transaction", ex);
            }
        }

        public void Dispose()
        {
            //delete instance of unit of work  if it is not used
            _context.Dispose();
        }

        /// <summary>
        /// Links the entity to a repository installation       
        /// The repository is the set of operations that you can perform on a given entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = _serviceProvider.GetRequiredService<IRepository<T>>();
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type]!;
        }
    }
}
