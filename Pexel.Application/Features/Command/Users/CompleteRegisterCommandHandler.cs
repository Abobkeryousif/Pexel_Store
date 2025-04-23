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

namespace Pexel.Application.Features.Command.Users
{
    public record CompleteRegisterCommand(UserOtp userOtp) : IRequest<HttpResponse<AuthenticationModel>>;
    public class CompleteRegisterCommandHandler : IRequestHandler<CompleteRegisterCommand, HttpResponse<AuthenticationModel>>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public CompleteRegisterCommandHandler(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IUserRepository userRepository, IOtpRepository otpRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _otpRepository = otpRepository;
        }

        public async Task<HttpResponse<AuthenticationModel>> Handle(CompleteRegisterCommand request, CancellationToken cancellationToken)
        {
            var otp = await _otpRepository.FirstOrDefaultAsync(o=> o.otp == request.userOtp.OTP);
            if (otp is null)
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.NotFound,"Plaese Enter True OTP ");

            if (otp.IsExpier)
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.BadRequest,"OTP Is Expier !");

            if (otp.IsUsed)
                return new HttpResponse<AuthenticationModel>(HttpStatusCode.BadRequest,"OTP Is Already Used");

            var user = await _userRepository.FirstOrDefaultAsync(u=> u.Email == otp.UserEmail);
            
            user.IsActive = true;
            otp.IsUsed = true;

            var refreshToken = _tokenService.AddRefreshToken();
            refreshToken.userId = user.Id;
            await _refreshTokenRepository.CreateAsync(refreshToken);

            var accessToken = _tokenService.Token(user);

            await _userRepository.UpdateAsync(user);
            await _otpRepository.UpdateAsync(otp);

            return new HttpResponse<AuthenticationModel>(HttpStatusCode.OK,"Seccuss Opration " , new AuthenticationModel 
            {
            accessToken = accessToken,
            refreshToken = refreshToken.Token,
            ExpierOn = refreshToken.ExpierOn
            });
        }
    }
}



