
using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Application.Contracts.Services;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Users;
using Pexel.Core.Entities;

using System.Net;


namespace Pexel.Application.Features.Command.Users
{
    public record RegisterUserCommand(UserDto userDto) : IRequest<HttpResponse<UserDto>>;
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, HttpResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpRepository _otpRepository;
        private readonly ISendEmail _sendEmail;
        

        public RegisterUserCommandHandler(IUserRepository userRepository, IOtpRepository otpRepository, ISendEmail sendEmail)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
            _sendEmail = sendEmail;
            
        }

        public async Task<HttpResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _userRepository.IsExist(u=> u.Email == request.userDto.Email);
            if (isExist)
                return new HttpResponse<UserDto>(HttpStatusCode.BadRequest,"This User Already Added!");

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.userDto.Password);
            var user = new User
            {
                FirstName = request.userDto.FirstName,
                LastName = request.userDto.LastName,
                Email = request.userDto.Email,
                Address = request.userDto.Address,
                City = request.userDto.City,
                Phone = request.userDto.Phone,
                Password = hashPassword
            };
            request.userDto.Password = user.Password;

            Random random = new Random();
            int otp = random.Next(0, 99999);

            var confiermOtp = new OTP
            {
                otp = otp.ToString("00000"),
                UserEmail = user.Email,
                ExpirationOn = DateTime.Now.AddMinutes(5),
                IsUsed = false,
            };
            
            _sendEmail.SendMail(user.Email , "Complete Register" , $"Your OTP To Complete Register:\n {confiermOtp.otp}");
            await _userRepository.CreateAsync(user);

            
            await _otpRepository.CreateAsync(confiermOtp);
            return new HttpResponse<UserDto>(HttpStatusCode.OK,$"We Send OTP To Your Email Mr: {user.UserName} Plaese Confierm It", request.userDto);
        }
    }
}





