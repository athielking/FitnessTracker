
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
            _db.Entry(userUpdate).State = EntityState.Modified;

            //// update user ref
            _db.Entry(userUpdate).Reference(u => u.Logs).IsModified = true;
            var x = _db.SaveChanges();

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
