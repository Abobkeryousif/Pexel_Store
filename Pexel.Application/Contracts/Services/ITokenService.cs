using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Contracts.Services
{
    public interface ITokenService
    {
       public string Token(User user);

       public RefreshToken AddRefreshToken();
    }
}
