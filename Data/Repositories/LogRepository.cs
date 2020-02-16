
using System.Collections.Generic;
using System.Linq;

using FitnessTracker.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly FitnessTrackerContext _db;

        public LogRepository(FitnessTrackerContext db)
        {
            _db = db;
        }

        public IEnumerable<Log> GetAllLogs()
        {
            var logs = _db.Logs
                .AsNoTracking()
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Include(user => user.User)
                .ToList();

            return logs;
        }

        public IEnumerable<Log> GetLogsByUserId(int id)
        {
            var logs = _db.Logs
                .AsNoTracking()
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Include(user => user.User)
                .Where(q => q.User.Id == id)
                .ToList();

            return logs;
        }

        public IEnumerable<Log> GetLogsByUserName(string username)
        {
            var logs = _db.Logs
                .AsNoTracking()
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Include(user => user.User)
                .Where( q => q.User.Username.Equals(username))
                .ToList();

            return logs;
        }

        public Log CreateLog(Log log)
        {
            _db.Attach(log).State = EntityState.Added;
            _db.SaveChanges();
            return log;
        }

        public int GetLogCount(string username)
        {
            var logs = _db.Logs
                .AsNoTracking()
                .Include(logex => logex.LogExercises)
                .ThenInclude(log => log.Exercise)
                .Include(user => user.User)
                .Where(q => q.User.Username.Equals(username))
                .ToList();

            return logs.Count();
        }

        public Log Update(Log logUpdate)
        {
            var logs = new List<LogExercise>(logUpdate.LogExercises);
            _db.Entry(logUpdate).State = EntityState.Modified;
           
            // replace all logs with updated log information
            _db.LogExercises.RemoveRange(_db.LogExercises.Where(obj => obj.LogId == obj.LogId ));

            // update logs with the updated log information
            foreach (var log in logs)
            {
                _db.Entry(log).State = EntityState.Added;
            }

            // update user ref
            _db.Entry(logUpdate).Reference(u => u.User).IsModified = true;
            _db.SaveChanges();

            return logUpdate;
        }

        public Log Delete(int id)
        {
            if (GetLogsByUserId(id) == null) return null;

            var results = _db.Remove(new Log { LogId = id }).Entity;
            _db.SaveChanges();
            return results;
        }
    }
}
