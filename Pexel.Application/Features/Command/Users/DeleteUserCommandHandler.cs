using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Command.Users
{
    public record DeleteUserCommand(int Id) : IRequest<HttpResponse<string>>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, HttpResponse<string>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefault(u=> u.Id == request.Id);
            if (user == null)
                return new HttpResponse<string>(HttpStatusCode.NotFound, $"Not Found With ID: {request.Id}");

            await _userRepository.DeleteAsync(user);
            return new HttpResponse<string>(HttpStatusCode.OK,"Seccess Delete Opration",user.UserName);
                 
        }
    }
}
