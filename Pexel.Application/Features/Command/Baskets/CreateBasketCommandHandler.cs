using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.Entities;
using System.Net;

namespace Pexel.Application.Features.Command.Baskets
{
    
    public record CreateBasketCommand(Basket basket) : IRequest<HttpResponse<Basket>>;
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, HttpResponse<Basket>>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<HttpResponse<Basket>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var customerBasket = await _basketRepository.UpdateBasketAsync(request.basket);
            return new HttpResponse<Basket>(HttpStatusCode.OK,"Basket Createed Seccussfly",customerBasket);
        }
    }
}
