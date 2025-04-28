using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Contracts.Interfaces
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<List<Basket>> GetAllBasketAsync();
        Task<Basket> GetBasketAsync(string id);
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
