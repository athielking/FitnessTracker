using FitnessTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
