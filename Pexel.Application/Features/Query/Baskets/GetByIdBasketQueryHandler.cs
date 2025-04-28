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
    public record GetByIdBasketQuery(string Id) : IRequest<HttpResponse<Basket>>;
    public class GetByIdBasketQueryHandler : IRequestHandler<GetByIdBasketQuery, HttpResponse<Basket>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetByIdBasketQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<HttpResponse<Basket>> Handle(GetByIdBasketQuery request, CancellationToken cancellationToken)
        {
          var result = await _basketRepository.GetBasketAsync(request.Id);
            if (result == null)
                return new HttpResponse<Basket>(HttpStatusCode.NotFound,$"Not Found Any Basket");
            return new HttpResponse<Basket>(HttpStatusCode.OK,"Seccuss Opration",result);
        }
    }
}
