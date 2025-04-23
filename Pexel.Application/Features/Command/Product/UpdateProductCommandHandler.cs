
using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Product;
using System.Net;


namespace Pexel.Application.Features.Command.Products
{
    public record UpdateProductCommand(int Id,ProductDto ProductDto) : IRequest<HttpResponse<ProductDto>>;
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, HttpResponse<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
           
        }

        public async Task<HttpResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p=> p.ProductId == request.Id);
            if (product == null)
                return new HttpResponse<ProductDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            product.Name = request.ProductDto.Name;
            product.Description = request.ProductDto.Description;
            product.OldPrice = request.ProductDto.OldPrice;
            product.NewPrice = request.ProductDto.NewPrice;
            product.CategoryId = request.ProductDto.CategoryId;
            await _productRepository.UpdateAsync(product);

            return new HttpResponse<ProductDto>(HttpStatusCode.OK,"Seccuss Update Opration",request.ProductDto);
            
        }
    }
}
