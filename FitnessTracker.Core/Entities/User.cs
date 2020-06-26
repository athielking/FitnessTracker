
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FitnessTracker.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Log> Logs { get; set; }
    }
}
