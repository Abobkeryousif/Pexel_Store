using BCrypt.Net;
using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Application.Contracts.Services;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Authencation
{
    public record LoginCommand(LoginUserDto loginUserDto) : IRequest<HttpResponse<AuthenticationModel>>;
    public class LoginCommandHandler : IRequestHandler<LoginCommand, HttpResponse<AuthenticationModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }
         public async Task<HttpResponse<AuthenticationModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var logUser = await _userRepository.FirstOrDefaultAsync(u=> u.Email == request.loginUserDto.Email);
            if (logUser == null)
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.NotFound, "Email or Password Is Invalid");

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.loginUserDto.Password , logUser.Password);
            if (!verifyPassword)
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.NotFound, "Email or Password Is Invalid");

            if (logUser.IsActive == false)
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.BadRequest, "Plaese Compelet Rigster To Login");

            var accessToken = _tokenService.Token(logUser);

            //Cheack if User Have Active Refresh Token And Pass By Old Refresh Token
            var UserrefreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(t=> t.userId == logUser.Id , o=> o.OrderByDescending(m=> m.CreatedOn));
            if (UserrefreshToken.IsActive)
            {
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.OK, $"Welcome MR: {logUser.UserName}", new AuthenticationModel
                {
                    accessToken = accessToken,
                    refreshToken = UserrefreshToken.Token,
                    ExpierOn = UserrefreshToken.ExpierOn,
                });
            }

            //Generate New Refresh Token To User
            var refreshToken = _tokenService.AddRefreshToken();
            refreshToken.userId = logUser.Id;

            await _refreshTokenRepository.CreateAsync(refreshToken);
            return new HttpResponse<AuthenticationModel>(HttpStatusCode.OK, $"Welcome MR: {logUser.UserName}",new AuthenticationModel 
            {
                accessToken = accessToken,
                refreshToken = refreshToken.Token,
                ExpierOn = refreshToken.ExpierOn,
            });



        }
    }
}
