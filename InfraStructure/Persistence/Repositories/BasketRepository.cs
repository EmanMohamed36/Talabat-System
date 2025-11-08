using DomainLayer.Contracts;
using DomainLayer.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    internal class BasketRepository : IBasketRepository
    {
        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasketAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket?> GetBasketAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
