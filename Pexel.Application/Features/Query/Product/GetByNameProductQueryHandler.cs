using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Products;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Query.Product
{
    public record GetByNameProductQuery(GetByNameProductDto productDto) : IRequest<HttpResponse<Productes>>;
    public class GetByNameProductQueryHandler : IRequestHandler<GetByNameProductQuery, HttpResponse<Productes>>
    {
        private readonly IProductRepository _productRepository;

        public GetByNameProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<HttpResponse<Productes>> Handle(GetByNameProductQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.productDto.ProductName))
                return new HttpResponse<Productes>(HttpStatusCode.BadRequest,"Please Enter Product Name");

            var product = await _productRepository.FirstOrDefaultAsync(p=> p.Name.ToLower() == request.productDto.ProductName.ToLower());
            if (product == null)
                return new HttpResponse<Productes>(HttpStatusCode.NotFound, $"Not Found Product With Name : {request.productDto.ProductName}");

            return new HttpResponse<Productes>(HttpStatusCode.OK, "Seccuss Opration", product);
        }
    }
}
