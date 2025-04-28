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

namespace Pexel.Application.Features.Command.Baskets
{
    public record DeleteBasketCommand(string Id) : IRequest<HttpResponse<string>>;
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, HttpResponse<string>>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<HttpResponse<string>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
          var result = await _basketRepository.DeleteBasketAsync(request.Id);
            return result ? new HttpResponse<string>(HttpStatusCode.OK, "Seccuss Delete Opration!") : new HttpResponse<string>(HttpStatusCode.NotFound,"Failed Opration");
        }
    }
}
