using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
