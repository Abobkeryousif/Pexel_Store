using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.Implementation
{
    public class OtpRepository : GenericRepository<OTP>, IOtpRepository
    {
        public OtpRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
