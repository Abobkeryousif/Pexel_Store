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

namespace Pexel.Application.Features.Query.Users
{
    public record GetUserQuery : IRequest<HttpResponse<List<GetUserDto>>>;
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, HttpResponse<List<GetUserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<HttpResponse<List<GetUserDto>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAllAsync();
            if (user.Count == 0)
                return new HttpResponse<List<GetUserDto>>(HttpStatusCode.NotFound,"Not Found Any User!");

            var userDto = user.Select(u=> new GetUserDto 
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                City = u.City,
                Address = u.Address,
                IsActive = u.IsActive,
            }).ToList();

            return new HttpResponse<List<GetUserDto>>(HttpStatusCode.OK,"Seccuss Opration",userDto);
        }
    }
}
