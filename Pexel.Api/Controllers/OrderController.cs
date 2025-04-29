using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pexel.Application.Contracts.Services;
using Pexel.Core.DTOs.Orders;
using Pexel.Core.Entities;
using System.Security.Claims;

namespace Pexel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)=>
        _orderServices = orderServices;
        

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(OrderDto orderDto) 
        {
            var customerEmail = GetCurrentUserEmail();
            if (customerEmail == null)
                return BadRequest("Not Found!");

            return Ok(await _orderServices.CreateOrder(orderDto,customerEmail));
        }

        [HttpGet("GetUserOrders")]
        public async Task<IActionResult> GetUserOrders() 
        {
            var customerEmail = GetCurrentUserEmail();
            if (customerEmail == null)
                return BadRequest("Not Found!");
            
            return Ok(await _orderServices.GetAllUserOrders(customerEmail));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetOrderById(int Id) 
        {
            var customerEmail = GetCurrentUserEmail();
            if (customerEmail == null)
                return BadRequest("Not Found!");
            return Ok(await _orderServices.GetOrderById(Id,customerEmail));
            
        }

         private string? GetCurrentUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }

    }

}
