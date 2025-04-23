using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Product
{
    public record DeleteProductCommand(int Id) : IRequest<HttpResponse<string>>;
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, HttpResponse<string>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;
      public async Task<HttpResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p=> p.ProductId == request.Id);
            if (product == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            await _productRepository.DeleteAsync(product);
            return new HttpResponse<string>(HttpStatusCode.OK,"Seccuss Delete Opration",product.Name);
        }
    }
}
