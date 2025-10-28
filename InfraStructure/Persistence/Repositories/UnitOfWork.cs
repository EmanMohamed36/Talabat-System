using DomainLayer.Contracts;
using DomainLayer.Models;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            }
            else
            { 
                var repo = new GenericRepository<TEntity,TKey>(_dbContext);
                _repositories.Add(typeName, repo);
                return repo;

            }
        }

        public async Task<int> SaveChangesAsync()
            =>  await _dbContext.SaveChangesAsync();
    }
}
