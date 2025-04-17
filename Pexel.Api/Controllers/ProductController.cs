using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using Pexel.Application.Features.Command.Product;
using Pexel.Application.Features.Command.Products;
using Pexel.Application.Features.Query.Product;
using Pexel.Core.DTOs.Product;
using Pexel.Core.DTOs.Products;

namespace Pexel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender) => _sender = sender;

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(AddProductDto productDto)
        {
            return Ok(await _sender.Send(new CreateProductCommand(productDto)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _sender.Send(new GetProductCommand()));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            return Ok(await _sender.Send(new GetByIdProductQuery(Id)));
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByNameAsync(GetByNameProductDto productDto)
        {
            return Ok(await _sender.Send(new GetByNameProductQuery(productDto)));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(int Id, ProductDto productDto)
        {
            return Ok(await _sender.Send(new UpdateProductCommand(Id, productDto)));
        }

        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteAsync(int Id) 
        {
            return Ok(await _sender.Send(new DeleteProductCommand(Id)));
        }
    }
}
    

