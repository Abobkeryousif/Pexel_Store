using MediatR;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Common;
using Pexel.Core.DTOs.Users;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Features.Query.Users
{
    public record GetByIdUserQuery(int Id) : IRequest<HttpResponse<GetUserDto>>;
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, HttpResponse<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetByIdUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<GetUserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u=> u.Id == request.Id);
            if (user == null)
                return new HttpResponse<GetUserDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.Id}");

            return new HttpResponse<GetUserDto>(HttpStatusCode.OK, "Seccuss Opration",new GetUserDto 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,   
                Email = user.Email,
                Phone = user.Phone,
                City = user.City,
                Address = user.Address,
                IsActive = user.IsActive
                
            });
        }
    }
}
