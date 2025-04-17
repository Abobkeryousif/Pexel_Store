using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Product;
using Pexel.Core.DTOs.Products;
using Pexel.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Product
{
    public record CreateProductCommand(AddProductDto productDto) : IRequest<HttpResponse<AddProductDto>>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, HttpResponse<AddProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<HttpResponse<AddProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _productRepository.IsExist(p=> p.Name.ToLower() == request.productDto.Name.ToLower());
            if (IsExist)
                return new HttpResponse<AddProductDto>(HttpStatusCode.BadRequest,"This Product Already Add!");

            //var product = new Productes
            //{
            //    Name = request.productDto.Name,
            //    Description = request.productDto.Description,
            //    OldPrice = request.productDto.OldPrice,
            //    NewPrice = request.productDto.NewPrice,
            //    CategoryId = request.productDto.CategoryId,
                
            //};

            await _productRepository.AddAsync(request.productDto);
            return new HttpResponse<AddProductDto>(HttpStatusCode.OK,"Seccuss Create Opration" ,request.productDto);

        }
    }
}
