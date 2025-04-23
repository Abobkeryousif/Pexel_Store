using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pexel.Application.Features.Command.Authencation;
using Pexel.Core.DTOs.Users;

namespace Pexel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;
        public AuthenticationController(ISender sender) =>
        _sender = sender;


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto) =>
            Ok(await _sender.Send(new LoginCommand(loginUserDto)));
        
    }
}
