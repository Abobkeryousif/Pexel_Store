using MailKit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pexel.Application.Features.Command.Users;
using Pexel.Application.Features.Query.Users;
using Pexel.Core.DTOs.Users;

namespace Pexel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> RegisterAsync(UserDto userDto) 
        {
            return Ok(await _sender.Send(new RegisterUserCommand(userDto)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAlAsync() 
        {
            return Ok(await _sender.Send(new GetUserQuery()));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(int Id) 
        {
            return Ok(await _sender.Send(new GetByIdUserQuery(Id)));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(int Id) 
        {
            return Ok(await _sender.Send(new DeleteUserCommand(Id)));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(int Id , UserDto userDto) 
        {
            return Ok(await _sender.Send(new UpdateUserCommand(Id,userDto)));
        }

        [HttpPost("CompleteRegister")]

        public async Task <IActionResult> CompleteRegisterAsync(UserOtp userOtp) 
        {
            return Ok(await _sender.Send(new CompleteRegisterCommand(userOtp)));
        }
    }
}




