using System.Collections.Generic;

using FitnessTracker.Core.Entities;

namespace FitnessTracker.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();        
        
        User GetById(int id);

        User GetByUsername(string username);

        User GetByUsernameWithAllLogs(string username);

        User Create(User user);

        User Update(User userUpdate);

        User Delete(int id);

        int Count();
    }
}
