using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Contracts.Services
{
    public interface ISendEmail
    {
        void SendMail(string mailTo , string Subject , string Message);
    }
}
