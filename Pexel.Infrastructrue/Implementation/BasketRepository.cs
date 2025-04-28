using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.Implementation
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(ApplicationDbContext context, IConnectionMultiplexer multiplexer) : base(context)
        {
            _database = multiplexer.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await _database.KeyDeleteAsync(id);    
        }

        public async Task<Basket> GetBasketAsync(string id)
        {
            var result = await _database.StringGetAsync(id);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<Basket>(result);
            }
            return null;
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var _basket = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
            if (_basket) { 
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }
    }
    }

