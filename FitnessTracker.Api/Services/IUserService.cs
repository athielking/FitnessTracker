using FitnessTracker.Core.Entities;
using FitnessTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Services
{
    public interface IUserService : IUserRepository
    {
        User SignIn(string username, string password);
    }
}
