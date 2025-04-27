using MediatR;
using Microsoft.Extensions.Configuration;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Application.Contracts.Services;
using Pexel.Core.Common;
using Pexel.Core.Common.Enum;
using Pexel.Core.DTOs.Users;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Authencation
{
    public record ForgetPasswordCommand(ForgetPasswordDto forgetPasswordDto) : IRequest<HttpResponse<string>>;
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, HttpResponse<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISendEmail _sendEmail;
        private readonly IVerificationRepository _verificationRepository;
        private readonly IConfiguration _configuration;

        public ForgetPasswordCommandHandler(IUserRepository userRepository, ISendEmail sendEmail, IVerificationRepository verificationRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _sendEmail = sendEmail;
            _verificationRepository = verificationRepository;
            _configuration = configuration;
        }
        public async Task<HttpResponse<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u=> u.Email == request.forgetPasswordDto.Email);
            if (user == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound,"Invalid Email");


            var verifiaction = new Verficiation
            {
                Email = request.forgetPasswordDto.Email,
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                tokenPerpouse = TokenPerpouse.RestPassword,
                ExpierOn = DateTime.Now.AddMinutes(30)
            };

            await _verificationRepository.CreateAsync(verifiaction);


            string url = $"{_configuration["AppUrl"]}/Authentication/ResetPassword?email={verifiaction.Email}&token={verifiaction.Token}";
            _sendEmail.SendMail(verifiaction.Email, "Reset Password", url);

            return new HttpResponse<string>(HttpStatusCode.OK, "Seccuss!", "We Send Url In Your Email To Reset Password");
        }
    }
}
