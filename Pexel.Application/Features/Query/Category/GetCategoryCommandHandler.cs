using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTO.Category;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Query.Category
{
    public record GetCategoryCommand : IRequest<HttpResponse<List<GetCategoryDto>>>;
    public class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, HttpResponse<List<GetCategoryDto>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<HttpResponse<List<GetCategoryDto>>> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetAllAsync();
            if (result.Count == 0)
                return new HttpResponse<List<GetCategoryDto>>(HttpStatusCode.NotFound, "Not Found Any Category!");
            var category = result.Select(c=> new GetCategoryDto { Id = c.Id , CategoryName = c.CategoryName}).ToList();

            return new HttpResponse<List<GetCategoryDto>>(HttpStatusCode.OK,"Sccuss!",category);

            
        }
    }
}
