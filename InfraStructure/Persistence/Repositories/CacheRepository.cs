using DomainLayer.Contracts;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string cackeKey)
        {
            var CachedValue = await _database.StringGetAsync(cackeKey);
            return CachedValue.IsNullOrEmpty? null : CachedValue.ToString();
        }

        public async Task SetAsync(string cackeKey, string value, TimeSpan timeToLive)
        {
           await _database.StringSetAsync(cackeKey,value, timeToLive);
        }
    }
}
