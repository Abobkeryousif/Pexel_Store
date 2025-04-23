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

namespace Pexel.Application.Features.Command.Users
{
    public record UpdateUserCommand(int Id , UserDto userDto) : IRequest<HttpResponse<UserDto>>;
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, HttpResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u=> u.Id == request.Id);
            if (user == null)
                return new HttpResponse<UserDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.userDto.Password);

            user.FirstName = request.userDto.FirstName;
            user.LastName = request.userDto.LastName;
            user.Email = request.userDto.Email;
            user.Phone = request.userDto.Phone;
            user.City = request.userDto.City;
            user.Address = request.userDto.Address;
            user.Password = hashPassword;

            await _userRepository.UpdateAsync(user);
            return new HttpResponse<UserDto>(HttpStatusCode.OK,"Seccuss Update Opration!",request.userDto);
        }
    }
}
