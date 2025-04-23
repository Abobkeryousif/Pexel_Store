using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Category
{
    public record DeleteCategoryCommand(int Id) : IRequest<HttpResponse<string>>;
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, HttpResponse<string>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResponse<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(c=> c.Id == request.Id);
            if (category == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            await _categoryRepository.DeleteAsync(category);
            return new HttpResponse<string>(HttpStatusCode.OK,"Seccuss Delete Opration",category.CategoryName);
        }
    }
}
