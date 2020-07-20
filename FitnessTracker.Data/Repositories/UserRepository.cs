using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using FitnessTracker.Core.Entities;

namespace FitnessTracker.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FitnessTrackerContext _db;

        public UserRepository(FitnessTrackerContext db)
        {
            _db = db;
        }

        public User GetById(string id)
        {
            return _db.User
               .AsNoTracking()
               .FirstOrDefault(c => c.Id.Equals(id));
        }

        public User GetByUsername(string username)
        {
            return _db.User
               .AsNoTracking()
               .FirstOrDefault(c => c.UserName.Equals(username));
        }

        public User GetByUsernameWithAllLogs(string username)
        {
            return _db.User
               .AsNoTracking()
               .Include( u => u.Logs)
               .FirstOrDefault(c => c.UserName.Equals(username));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _db.User.ToList();
        }

        public User Create(User user)
        {
            var userSaved = _db.User.Add(user).Entity;
            _db.SaveChanges();
            return userSaved;
        }

        public User Update(User user)
        {
            _db.Update(user);

            //// update user ref
            _db.Entry(user).Reference(u => u.Logs).IsModified = true;
            var x = _db.SaveChanges();

            return user;
        }

        public User Delete(string id)
        {
            var user = GetById(id);
            if ( user == null) return null;

            var custRemoved = _db.Remove(user).Entity;
            _db.SaveChanges();
            return custRemoved;
        }

        public int Count()
        {
            return _db.User.Count();
        }
    }
}
