using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.Implementation
{
    public class VerificationRepository : GenericRepository<Verficiation>, IVerificationRepository
    {
        public VerificationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
