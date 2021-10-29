using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Models
{
    public class UserAD
    {
        public string Id { get; set; }
        public string UserPrincipalName { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
    }
}
