
using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTO.Category;
using Pexel.Core.Entities;
using System.Net;

namespace Pexel.Application.Features.Command.Category
{
    public record CreateCategoryCommand(CategoryDto CategoryDto) : IRequest<HttpResponse<CategoryDto>>;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, HttpResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _categoryRepository.IsExist(c => c.CategoryName == request.CategoryDto.CategoryName);
            if (isExist)
                return new HttpResponse<CategoryDto>(HttpStatusCode.BadRequest,"This Category Already Added");

            var category = new Categories
            {
                CategoryName = request.CategoryDto.CategoryName
            };

            await _categoryRepository.CreateAsync(category);
            return new HttpResponse<CategoryDto>(HttpStatusCode.OK, "Category Added Sccussfaly!",request.CategoryDto);
        }
    }
}
