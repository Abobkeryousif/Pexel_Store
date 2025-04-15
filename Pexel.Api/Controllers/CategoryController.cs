using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pexel.Application.Features.Command.Category;
using Pexel.Application.Features.Query.Category;
using Pexel.Core.DTO.Category;

namespace Pexel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CategoryDto categoryDto) 
        {
            return Ok(await _sender.Send(new CreateCategoryCommand(categoryDto)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            return Ok(await _sender.Send(new GetCategoryCommand()));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int Id) 
        {
            return Ok(await _sender.Send(new GetByIdCategoryCommand(Id)));
        }

        [HttpPut("Update")]

        public async Task<IActionResult> UpdateAsync(int Id,CategoryDto categoryDto) 
        {
            return Ok(await _sender.Send(new UpdateCategoryCommand(Id,categoryDto)));
        }
        [HttpDelete("Delete")]

        public async Task<IActionResult> DeleteAsync(int Id)
        {
            return Ok(await _sender.Send(new DeleteCategoryCommand(Id)));
        }

    }
}
