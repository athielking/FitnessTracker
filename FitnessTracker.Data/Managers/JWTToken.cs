using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Models
{
    public class JWTToken
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Expiration { get; set; }
    }
}
