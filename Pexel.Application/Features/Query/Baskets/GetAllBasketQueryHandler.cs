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

namespace Pexel.Application.Features.Query.Baskets
{
    public record GetAllBasketQuery : IRequest<HttpResponse<List<Basket>>>;
    public class GetAllBasketQueryHandler : IRequestHandler<GetAllBasketQuery, HttpResponse<List<Basket>>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetAllBasketQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<HttpResponse<List<Basket>>> Handle(GetAllBasketQuery request, CancellationToken cancellationToken)
        {
            var baskets = await _basketRepository.GetAllBasketAsync();
            if (baskets.Count == 0)
                return new HttpResponse<List<Basket>>(HttpStatusCode.NotFound,"Not Found Any Baskets");

            return new HttpResponse<List<Basket>>(HttpStatusCode.OK,"Seccuss Opration", baskets);

        }
    }
}
