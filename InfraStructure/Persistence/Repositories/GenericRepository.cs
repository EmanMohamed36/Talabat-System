using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext)
                    : IGenericRepository<TEntity, TKey>
                    where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) 
            => await _dbContext.Set<TEntity>().AddAsync(entity);
            

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbContext.Set<TEntity>().ToListAsync();
        

        public async Task<TEntity?> GetByIdAsync(TKey id)
        
            => await _dbContext.Set<TEntity>().FindAsync(id);
        public void Remove(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);
            

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        #region With Specification
        async Task<IEnumerable<TEntity>> IGenericRepository<TEntity, TKey>.GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpeceficationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
        }

        async Task<TEntity?> IGenericRepository<TEntity, TKey>.GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpeceficationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        async Task<int> IGenericRepository<TEntity, TKey>.GetCountSpecificationAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpeceficationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();

        }
        #endregion
    }
}
