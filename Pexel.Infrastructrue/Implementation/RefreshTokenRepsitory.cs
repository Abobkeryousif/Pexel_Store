using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.Implementation
{
    public class RefreshTokenRepsitory : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepsitory(ApplicationDbContext context) : base(context)
        {
        }
    }
}
