using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Extenisons
{
    public static class UserExtenison
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var x = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            var userId= principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            ?? throw new ArgumentNullException("userid", "Unauthorized Access");

            return userId;
        }
    }
}
