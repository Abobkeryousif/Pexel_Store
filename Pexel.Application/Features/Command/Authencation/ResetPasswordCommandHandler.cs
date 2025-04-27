using MediatR;
using Pexel.Application.Contracts.Interfaces;
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
    public record ResetPasswordCommand(ResetPasswordDto resetPasswordDto) : IRequest<HttpResponse<string>>;
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, HttpResponse<string>>
    {
        private readonly IVerificationRepository _verificationRepository;
        private readonly IUserRepository _userRepository;
        public ResetPasswordCommandHandler(IUserRepository userRepository, IVerificationRepository verificationRepository)
        {
            _userRepository = userRepository;
            _verificationRepository = verificationRepository;
        }
        public async Task<HttpResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var User = await _userRepository.FirstOrDefaultAsync(u=> u.Email == request.resetPasswordDto.email);
            if (User == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound , $"Not Found User With Email: {request.resetPasswordDto.email}");

            var verificationUser = await _verificationRepository.FirstOrDefaultAsync(v=> v.Email == User.Email);
            if (verificationUser.Token != request.resetPasswordDto.token)
                return new HttpResponse<string>(HttpStatusCode.BadRequest,"Invalid Token");

            if (verificationUser.IsExpier)
                return new HttpResponse<string>(HttpStatusCode.BadRequest,"Expier Token");

            if (verificationUser.IsUsed)
                return new HttpResponse<string>(HttpStatusCode.BadRequest,"This Token Already Used");

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.resetPasswordDto.password);
            User.Password = hashPassword;
            await _userRepository.UpdateAsync(User);

            verificationUser.IsUsed = true;

            await _verificationRepository.UpdateAsync(verificationUser);

            return new HttpResponse<string>(HttpStatusCode.OK,"Seccuss!",$"Password Updated Seccussfly {User.UserName}");
        }
    }
}
