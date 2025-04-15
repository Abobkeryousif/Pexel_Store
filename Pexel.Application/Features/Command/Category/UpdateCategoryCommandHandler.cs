
using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTO.Category;
using Pexel.Core.Entities;
using System.Net;

namespace Pexel.Application.Features.Command.Category
{
    public record UpdateCategoryCommand(int Id , CategoryDto categoryDto) : IRequest<HttpResponse<Categories>>;
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, HttpResponse<Categories>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResponse<Categories>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FirstOrDefault(c=> c.Id == request.Id);
            if (result == null)
                return new HttpResponse<Categories>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");
            result.CategoryName = request.categoryDto.CategoryName;
            var category = new Categories
            { CategoryName = result.CategoryName};
            await _categoryRepository.UpdateAsync(result);

            return new HttpResponse<Categories>(HttpStatusCode.OK,"Seccussfly Opration",category);


        }
    }
}
