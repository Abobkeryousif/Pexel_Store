using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Product;
using Pexel.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Product
{
    public record CreateProductCommand(ProductDto productDto) : IRequest<HttpResponse<ProductDto>>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, HttpResponse<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<HttpResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _productRepository.IsExist(p=> p.Name.ToLower() == request.productDto.Name.ToLower());
            if (IsExist)
                return new HttpResponse<ProductDto>(HttpStatusCode.BadRequest,"This Product Already Add!");

            var product = new Productes
            {
                Name = request.productDto.Name,
                Description = request.productDto.Description,
                OldPrice = request.productDto.OldPrice,
                NewPrice = request.productDto.NewPrice,
                Quantity = request.productDto.Quantity,
                CategoryId = request.productDto.CategoryId,
            };

            await _productRepository.CreateAsync(product);
            return new HttpResponse<ProductDto>(HttpStatusCode.OK,"Seccuss Create Opration" , request.productDto);

        }
    }
}
