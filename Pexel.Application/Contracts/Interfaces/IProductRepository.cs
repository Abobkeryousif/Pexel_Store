using Pexel.Core.DTOs.Product;
using Pexel.Core.DTOs.Products;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Contracts.Interfaces
{
    public interface IProductRepository : IGenericRepository<Productes>
    {
        Task<bool> AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);

        Task DeleteAsyncs(Productes product);
    }
}
