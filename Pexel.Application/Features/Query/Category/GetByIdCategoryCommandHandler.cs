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

namespace Pexel.Application.Features.Query.Category
{
    public record GetByIdCategoryCommand(int Id) : IRequest<HttpResponse<Categories>>;
    public class GetByIdCategoryCommandHandler : IRequestHandler<GetByIdCategoryCommand, HttpResponse<Categories>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetByIdCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResponse<Categories>> Handle(GetByIdCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.FirstOrDefault(c => c.Id == request.Id);
            if (result == null)
                return new HttpResponse<Categories>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            return new HttpResponse<Categories>(HttpStatusCode.OK,"Sccuss!",result);
            

        }
    }
}
