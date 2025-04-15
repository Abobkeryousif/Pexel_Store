using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Query.Product
{
    public record GetByIdProductQuery(int Id) : IRequest<HttpResponse<Productes>>;
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, HttpResponse<Productes>>
    {
        private readonly IProductRepository _productRepository;

        public GetByIdProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<HttpResponse<Productes>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FirstOrDefault(p=> p.ProductId == request.Id);
            if (product == null)
                return new HttpResponse<Productes>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");
            return new HttpResponse<Productes>(HttpStatusCode.OK,"Seccuss Opration",product);
        }
    }
}
