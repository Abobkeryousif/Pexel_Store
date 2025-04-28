using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Application.Features.Command.Baskets;
using Pexel.Application.Features.Query.Baskets;
using Pexel.Core.Entities;

namespace Pexel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly ISender _sender;

        public BasketController(ISender sender)=>
            _sender = sender;
        

        [HttpGet("GetById")]
        public async Task<IActionResult> GetBasketAsync(string Id) =>
            Ok(await _sender.Send(new GetByIdBasketQuery(Id)));
        
        [HttpPost("CreateBasket")]
        public async Task<IActionResult> CreateBasketAsync(Basket basket) =>
         Ok(await _sender.Send(new CreateBasketCommand(basket)));


        [HttpDelete("DeleteBasket")]
        public async Task<IActionResult> DeleteBasketAsync(string Id) =>
            Ok(await _sender.Send(new DeleteBasketCommand(Id)));

        [HttpGet]
        public async Task<IActionResult> GetAllBasketAsync() =>
            Ok(await _sender.Send(new GetAllBasketQuery()));
    }
}
