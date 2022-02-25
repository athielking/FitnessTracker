using System;
using System.Linq;
using System.Diagnostics;
using System.Security.Claims;

using Microsoft.Identity.Web;

namespace FitnessTracker.Api.Extenisons
{
    public static class UserExtenison
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var userName = principal.Claims.First(c => c.Type == ClaimTypes.Name);
            Debug.WriteLine($"The user name is {userName.Value}");

            var userId = principal.FindFirst(c => c.Type == ClaimConstants.ObjectId)?.Value
            ?? throw new ArgumentNullException("userid", "Unauthorized Access");

            return userId;
        }
    }
}
