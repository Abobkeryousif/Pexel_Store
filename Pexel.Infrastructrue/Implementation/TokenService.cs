using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pexel.Application.Contracts.Services;
using Pexel.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Pexel.Infrastructrue.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Refresh Token Method
        public RefreshToken AddRefreshToken()
        {
            var random = new byte[32];
            using var Generator = new RNGCryptoServiceProvider();
            Generator.GetBytes(random);
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Convert.ToBase64String(random),
                CreatedOn = DateTime.Now,
                ExpierOn = DateTime.Now.AddDays(3),
            };

        }

        //JWT Bearer Method
        public string Token(User user)
        {
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()));
            claim.Add(new Claim(ClaimTypes.Email, user.Email));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
            var Cerdintial = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                _configuration["Token:Issure"],
                _configuration["Token:Audience"],
                claim,
                signingCredentials : Cerdintial,
                expires : DateTime.Now.AddMinutes(45)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
