
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using FitnessTracker.Data.Entities;

namespace FitnessTracker.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FitnessTrackerContext _db;

        public UserRepository(FitnessTrackerContext db)
        {
            _db = db;
        }

        public User GetById(int id)
        {
            return _db.User
               .AsNoTracking()
               .FirstOrDefault(c => c.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _db.User
               .AsNoTracking()
               .FirstOrDefault(c => c.Username.Equals(username));
        }

        public User GetByUsernameWithAllLogs(string username)
        {
            return _db.User
               .AsNoTracking()
               .Include( u => u.Logs)
               .FirstOrDefault(c => c.Username.Equals(username));
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

        public User Update(User userUpdate)
        {
            _db.Attach(userUpdate).State = EntityState.Modified;
            _db.Entry(userUpdate).Collection(c => c.Logs).IsModified = true;

            var logs = _db.Logs.Where( o => o.UserId == userUpdate.Id);

            foreach (var log in logs)
            {
                log.User = null;
                _db.Entry(log).Reference(o => o.User).IsModified = true;
            }

            _db.SaveChanges();
            return userUpdate;
        }

        public User Delete(int id)
        {
            if (GetById(id) == null) return null;

            var custRemoved = _db.Remove(new User { Id = id }).Entity;
            _db.SaveChanges();
            return custRemoved;
        }

        public int Count()
        {
            return _db.User.Count();
        }
    }
}
