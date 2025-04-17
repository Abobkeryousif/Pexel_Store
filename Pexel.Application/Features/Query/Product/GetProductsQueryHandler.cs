
using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Product;
using Pexel.Core.DTOs.Products;
using System.Net;

namespace Pexel.Application.Features.Query.Product
{
    public record GetProductCommand : IRequest<HttpResponse<List<GetProductDto>>> ;
    public class GetProductsQueryHandler : IRequestHandler<GetProductCommand, HttpResponse<List<GetProductDto>>>
    {
        private readonly IProductRepository _ProductRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }

        public async Task<HttpResponse<List<GetProductDto>>> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetAllAsync();
            if (product.Count == 0)
                return new HttpResponse<List<GetProductDto>>(HttpStatusCode.NotFound,"Not Found Any Product");

            var productDto = product.Select(p => new GetProductDto
            {
                Id = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                NewPrice = p.NewPrice,
                OldPrice = p.OldPrice,
                CategoryId = p.CategoryId,
            }).ToList();
            return new HttpResponse<List<GetProductDto>>(HttpStatusCode.OK, "Seccuss Opration",productDto); 
            
        }
    }
}
