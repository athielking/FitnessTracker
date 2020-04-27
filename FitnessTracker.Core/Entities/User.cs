﻿
using System.Collections.Generic;

namespace FitnessTracker.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<Log> Logs { get; set; }
    }
}